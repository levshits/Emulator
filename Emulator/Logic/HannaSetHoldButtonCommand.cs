using Emulator.Logic.Hanna;

namespace Emulator.Logic
{
    public class HannaSetHoldButtonCommand: CommandWithDelay
    {
        public HannaStateMachine HannaStateMachine { get; set; }
        protected override void TimerHandler(object state)
        {
        }
    }
}