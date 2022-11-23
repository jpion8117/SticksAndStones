using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.StatusEffects;
using System;

namespace SticksAndStones.Models.GameComponents.Moves.Healer
{
    public class MedicalMalpractice : BaseMove
    {
        private int _rounds;
        private int _damagePerRound = 6;
        private int _maxTargets = 1;
        private int _moveCost = 5;
        bool _lawsuit = false;

        public MedicalMalpractice(CharacterBase executioner) : base(executioner)
        {
            var rng = new Random();
            int num = rng.Next(1, 100);

            if (num > 85)
            {
                _rounds = 3;
            }
            else if (num < 5)
            {
                _rounds = 1;
            }
            else if (num == 1)
            {
                _lawsuit = true; //poisons you for one round instead
                _rounds = 1;
            }
            else
            {
                _rounds = 2;
            }
        }

        public override int MoveCost 
        { 
            get => _moveCost; 
            protected set => _moveCost = value; 
        }
        public override int MaxTargets 
        { 
            get => _maxTargets; 
            protected set => _maxTargets = value;
        }
        public override string DisplayName => "Medical Malpractice";
        public override string FlavorText => $"Just because they're your enemy doesn't mean you can't 'help' them out " +
            $"some... Mistakes do happen... Poison an enemy for 1-3 turns dealing 6hp defense ignoring damage. Beware " +
            $"of lawsuits though. (1/100 chance of poisoning yourself) --{_moveCost}POW";

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (mode == ProcessMode.Move)
            {
                if (!CheckIfValidMove())
                    return GameError.MOVE_INVALID;

                CharacterBase target;

                if (_lawsuit)
                    target = _moveExecutioner;
                else
                    target = _targets[0];

                var poison = new Poison(target, _rounds, _damagePerRound);

                target.AddStatusEffect(poison);

                return base.ExecuteCommonAction();
            }

            return GameError.SUCCESS;
        }
    }
}
