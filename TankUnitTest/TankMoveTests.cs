using Microsoft.VisualStudio.TestTools.UnitTesting;
using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.Moves.Tank;
using SticksAndStones.Models.GameComponents.Moves;

namespace TankUnitTest
{
    [TestClass]
    public class TankMoveTests
    {
        /// <summary>
        /// Tests the standard attack against an opponent with no defense multiplier
        /// </summary>
        [TestMethod]
        public void Test001_StandardAttackNoDefense()
        {
            var executioner = new Tank();
            var target = new Tank();
            target.DefenseMultiplier = 0;
            var baseAttack = new StandardAttack(executioner);
            baseAttack.AddTarget(target);
            baseAttack.ExecuteAction();

            int expectedResult = 94;
            Assert.IsTrue(baseAttack.AttackDamage == 6, $"Attack damage: {baseAttack.AttackDamage} " +
                $"Expected: 6");
            Assert.IsTrue(target.Health == expectedResult, $"Expected health to be {expectedResult} but " +
                $"reported as {target.Health}");
        }
        /// <summary>
        /// Tests the standard attack against an opponent with a defense multiplier
        /// </summary>
        [TestMethod]
        public void Test002_StandardAttackWithDefense()
        {
            var executioner = new Tank();
            var target = new Tank();
            var baseAttack = new StandardAttack(executioner);
            baseAttack.AddTarget(target);
            baseAttack.ExecuteAction();

            int expectedResult = 97;
            Assert.IsTrue(baseAttack.AttackDamage == 6, $"Attack damage: {baseAttack.AttackDamage} " +
                $"Expected: 6");
            Assert.IsTrue(target.Health == expectedResult, $"Expected health to be {expectedResult} but " +
                $"reported as {target.Health}");
        }
        /// <summary>
        /// Tests the bullet sponge move and by extension the attack redirect system
        /// </summary>
        [TestMethod]
        public void Test003_BulltetSpongeMove()
        {
            //create multiple instances of Tank class
            var executioner = new Tank();
            var teamMember1 = new Tank();
            var teamMember2 = new Tank();

            //create instance of bullet sponge move
            var bulletSponge = new BulletSponge(executioner);

            //create a fake party identifieer and assign it to all 3 tanks so they 
            //can be identified as part of the same party
            ulong partyID = 1000;
            executioner.PartyID = partyID;
            teamMember1.PartyID = partyID;
            teamMember2.PartyID = partyID;

            //add the two team members to the move and execute the move
            bulletSponge.AddTarget(teamMember1);
            bulletSponge.AddTarget(teamMember2);
            bulletSponge.ExecuteAction();

            //verify move was applied properly
            Assert.IsTrue(bulletSponge.CheckIfValidMove(), "Move not returning valid");
            Assert.IsTrue(teamMember1.RedirectAttackTarget != null, "Redirect failed to bind");

            //apply 10hp direct damage to all 3 Tanks, damage should all be directed to executioner
            teamMember1.TakeDamage(10, true);
            teamMember2.TakeDamage(10, true);
            executioner.TakeDamage(10, true);

            //verify all damage was redirected to executioner
            int expectedResultE = 70; //expected health of the executioner
            int expectedResultT = 100; //expected health of the team members
            Assert.IsTrue(teamMember1.Health == expectedResultT, $"teamMember1 health should be " +
                $"{expectedResultT}HP but was reported as {teamMember1.Health}HP");
            Assert.IsTrue(teamMember2.Health == expectedResultT, $"teamMember1 health should be " +
                $"{expectedResultT}HP but was reported as {teamMember2.Health}HP");
            Assert.IsTrue(executioner.Health == expectedResultE, $"teamMember1 health should be " +
                $"{expectedResultE}HP but was reported as {executioner.Health}HP");

            //run ExecuteAction on teamMember1 to remove redirect then apply 10hp direct damage
            teamMember1.ExecuteAction();
            teamMember1.TakeDamage(10, true);

            //verify damage was approprately applied to only teamMember1 and not executioner
            expectedResultT = 90;//new expected result for teamMember1
            Assert.IsTrue(teamMember1.Health == expectedResultT, $"teamMember1 health should be " +
                $"{expectedResultT}HP but was reported as {teamMember1.Health}HP");
            Assert.IsTrue(executioner.Health == expectedResultE, $"teamMember1 health should be " +
                $"{expectedResultE}HP but was reported as {executioner.Health}HP");
        }
    }
}
