﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Logging;
using Emulator.Common.Interfaces;
using Emulator.Common.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Emulator.Data
{
    /// <summary>
    /// Класс работы с внешними данными.
    /// Реализуется поддержка работы с Excel.
    /// Стоило вынести имя файла в конфигурацию приложения
    /// </summary>
    public class DataProvider: IDataProvider
    {
        private VariantModel _data;
        private List<FilterModel> _filters = new List<FilterModel>();
        private List<VariantModel> _variants = new List<VariantModel>();
        protected static readonly ILog Log = LogManager.GetLogger<IDataProvider>();
        
        private const string DATA_FILE = @"./Config/Data.xls";
        public void ApplyFilter(FilterModel filter)
        {
            _data.Temperature = _data.Temperature*filter.TemperatureCoefficient;
            _data.Oxygen = _data.Oxygen * filter.OxygenCoefficient;
            _data.EC = _data.EC * filter.ECCoefficient;
            _data.TDS = _data.TDS * filter.TDSCoefficient;
            _data.PH = _data.PH * filter.PHCoefficient;
            DataChanged?.Invoke(this, null);
        }

        public void Reset(VariantModel variant)
        {
            if (variant == null)
            {
                throw new ArgumentNullException(nameof(variant));
            }
            _data = variant.CreateCopy();
            DataChanged?.Invoke(this, null);
        }

        public List<VariantModel> GetVariants()
        {
            return _variants.Select(x=>x.CreateCopy()).ToList();
        }

        public List<FilterModel> GetFilters()
        {
            return _filters;
        }

        public void OnInit()
        {
            IWorkbook wb = null;
            try
            {
                Log.Debug("Initialization started");
                using (FileStream file = new FileStream(DATA_FILE, FileMode.Open, FileAccess.Read))
                {
                    wb = new HSSFWorkbook(file);
                }
                _filters = LoadSheet<FilterModel>("Filters", wb);
                _variants = LoadSheet<VariantModel>("Variants", wb);
                if (!_variants.Any())
                {
                    throw new ArgumentException("Variants sheet should not be empty");
                }
                _data = _variants.First().CreateCopy();
            }
            catch (Exception e)
            {
                Log.Error("Initialization exception", e);
                throw;
            }
        }

        public event EventHandler DataChanged;
        public VariantModel GetData()
        {
            return _data.CreateCopy();
        }

        static List<T> LoadSheet<T>(string sheetName, IWorkbook wb) where T : new()
        {
            var list = new List<T>();
            Dictionary<int, string> columns = null;
            Dictionary<string, PropertyInfo> propertiesDictionary = new Dictionary<string, PropertyInfo>();
            ISheet filtersSheet = wb.GetSheet(sheetName);
            foreach (IRow row in filtersSheet)
            {
                if (columns == null)
                {
                    columns = new Dictionary<int, string>();
                    foreach (ICell cell in row.Cells)
                    {
                        columns.Add(cell.ColumnIndex, cell.StringCellValue);
                    }
                }
                else
                {
                    var item = new T();
                    
                    var type = item.GetType();
                    foreach (ICell cell in row.Cells)
                    {
                        PropertyInfo propertyInfo = null;
                        if (propertiesDictionary.ContainsKey(columns[cell.ColumnIndex]))
                        {
                            propertyInfo = propertiesDictionary[columns[cell.ColumnIndex]];
                        }
                        else
                        {
                            propertyInfo = type.GetProperty(columns[cell.ColumnIndex]);
                            propertiesDictionary.Add(columns[cell.ColumnIndex], propertyInfo);
                        }

                        switch (cell.CellType)
                        {
                            case CellType.Blank:
                                propertyInfo.SetValue(item, null);
                                break;
                            case CellType.Boolean:
                                propertyInfo.SetValue(item, cell.BooleanCellValue);
                                break;
                            case CellType.Numeric:
                                propertyInfo.SetValue(item, Convert.ChangeType(cell.NumericCellValue, propertyInfo.PropertyType));
                                break;
                            case CellType.String:
                                propertyInfo.SetValue(item, cell.StringCellValue);
                                break;
                            case CellType.Error:
                                propertyInfo.SetValue(item, cell.ErrorCellValue);
                                break;
                            case CellType.Formula:
                            default:
                                propertyInfo.SetValue(item, "="+cell.CellFormula);
                                break;
                        }
                    }
                    list.Add(item);
                }
            }
            return list;
        } 
    }
}