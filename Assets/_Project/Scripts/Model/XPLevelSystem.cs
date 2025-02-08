using UniRx;

namespace _Project.Scripts.Model
{
    public class XpLevelSystem
    {
        public ReactiveProperty<int> Level { get; private set; }
        public ReactiveProperty<int> CurrentExp { get; private set; }
        public int ExpToLevelUp { get; private set; } = 10;
        public int TotalExp { get; private set; } = 0;

        public XpLevelSystem()
        {
            Level = new ReactiveProperty<int>(1);
            CurrentExp = new ReactiveProperty<int>(0);
        }
        
        public void AddExp(int exp)
        {
            if (exp <= 0) return;
            
            TotalExp += exp;
            CurrentExp.Value += exp;

            if (CurrentExp.Value < ExpToLevelUp) return;
            
            CurrentExp.Value -= ExpToLevelUp;
            ExpToLevelUp += 20;
            Level.Value += 1;
        }
    }
}