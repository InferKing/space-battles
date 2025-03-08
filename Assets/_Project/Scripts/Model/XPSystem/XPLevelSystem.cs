using UniRx;

namespace _Project.Scripts.Model.XPSystem
{
    public class XpLevelSystem
    {
        public ReactiveProperty<int> CurrentExp { get; } = new(0);
        public ReactiveProperty<int> FreePoints { get; } = new(0);
        public int ExpToLevelUp { get; private set; } = 10;
        public int TotalExp { get; private set; }
        public int Level { get; private set; }

        public void AddExp(int exp)
        {
            if (exp <= 0) return;
            
            TotalExp += exp;
            CurrentExp.Value += exp;

            if (CurrentExp.Value < ExpToLevelUp) return;
            
            CurrentExp.Value -= ExpToLevelUp;
            ExpToLevelUp += 20;
            Level += 1;
            FreePoints.Value += 1;
        }
        
    }
}