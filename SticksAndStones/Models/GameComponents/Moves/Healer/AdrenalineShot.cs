using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.StatusEffects;
using System;

namespace SticksAndStones.Models.GameComponents.Moves.Healer
{
    public class AdrenalineShot : BaseMove
    {
        private int _moveCost = 8;
        private int _maxTargets = 1;
        private int _healthPerRound;
        private double _attackBoost = 0.2;
        private int _numberOfRoundsHealth = 3;
        private int _numberOfRoundsBoost = 1;

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
        public override string DisplayName => "Adrenaline Shot";
        public override string FlavorText => $"Give your team mate a shot of artificial adrenaline to get them " +
            $"back in the game! Heal a team mate 8% of their total health per round for {_numberOfRoundsHealth} " +
            $"rounds and boost their attack by {Math.Round(_attackBoost * 100, 0)}% per round for " +
            $"{_numberOfRoundsBoost}. --{_moveCost}POW";

        public AdrenalineShot(CharacterBase executioner) : base(executioner) { }

        public override GameError ExecuteAction(ProcessMode mode = ProcessMode.Move)
        {
            if (mode == ProcessMode.Move)
            {
                if (CheckIfValidMove())
                {
                    //create status effects
                    var hOT = new HealthOverTime(_targets[0], 3, (int)((double)_targets[0].MaxHealth * 0.8));
                    var aBoost = new AttackBoost(_targets[0], .2, 1);

                    //apply status effects
                    _targets[0].AddStatusEffect(hOT);
                    _targets[0].AddStatusEffect(aBoost);

                    _processModes[ProcessMode.Move] = false;

                    return base.ExecuteCommonAction();
                }
                else
                    return GameError.MOVE_INVALID;
            }

            return GameError.SUCCESS;
        }
    }
}
