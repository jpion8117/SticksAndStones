using Microsoft.VisualStudio.TestTools.UnitTesting;
using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.Moves.Tank;
using SticksAndStones.Models.GameComponents.Moves;
using SticksAndStones.Models.GameComponents;
using System;

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
        [TestMethod]
        public void Test003_StandardBlock()
        {
            var executioner = new Tank();
            var move = new StandardBlock(executioner);

            move.ExecuteAction(ProcessMode.Move);
            move.ExecuteAction(ProcessMode.Turn);
            Assert.IsFalse(move.Completed, "Move was prematurely marked as complete");
            double expectedMultiplier = Math.Round(1 - 0.8f, 2); 
            Assert.IsTrue(executioner.DefenseMultiplier == expectedMultiplier, $"Defense Multiplier expected: {expectedMultiplier}," +
                $" result: {executioner.DefenseMultiplier}");

            //cause it pain...
            executioner.TakeDamage(10);

            int expectedHealth = 98;
            Assert.IsTrue(executioner.Health == expectedHealth, $"Health Expected: {expectedHealth}," +
                $" result: {executioner.Health}");

            expectedMultiplier = 0.5f;
            move.ExecuteAction(ProcessMode.Round);
            Assert.IsTrue(executioner.DefenseMultiplier == expectedMultiplier, $"Defense Multiplier expected: {expectedMultiplier}," +
                $" result: {executioner.DefenseMultiplier}");
            Assert.IsTrue(move.Completed, "move failed to be marked completed");
        }
        /// <summary>
        /// Tests the bullet sponge move and by extension the attack redirect system
        /// </summary>
        [TestMethod]
        public void Test004_BulltetSpongeMove()
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
            int expectedPowRemain = 5;
            Assert.IsTrue(bulletSponge.CheckIfValidMove(), "Move not returning valid");
            Assert.IsTrue(executioner.Power == expectedPowRemain, $"executioner should have {expectedPowRemain} power " +
                $"remaining, but reports {executioner.Power} power");
            Assert.IsTrue(teamMember1.RedirectAttackTarget != null, "Redirect failed to bind");

            //apply 10hp direct damage to all 3 Tanks, damage should all be directed to executioner
            teamMember1.TakeDamage(10, true);
            teamMember2.TakeDamage(10, true);
            executioner.TakeDamage(10, true);

            teamMember1.ExecuteAction();
            teamMember1.ExecuteAction(ProcessMode.Turn);
            teamMember2.ExecuteAction();
            teamMember2.ExecuteAction(ProcessMode.Turn);

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
            teamMember1.ExecuteAction(ProcessMode.Round);
            teamMember1.TakeDamage(10, true);

            //verify damage was approprately applied to only teamMember1 and not executioner
            expectedResultT = 90;//new expected result for teamMember1
            Assert.IsTrue(teamMember1.Health == expectedResultT, $"teamMember1 health should be " +
                $"{expectedResultT}HP but was reported as {teamMember1.Health}HP");
            Assert.IsTrue(executioner.Health == expectedResultE, $"teamMember1 health should be " +
                $"{expectedResultE}HP but was reported as {executioner.Health}HP");
        }
        /// <summary>
        /// Tests the blood donor move inwich one tank character gives another character 10hp 
        /// </summary>
        [TestMethod]
        public void Test005_BloodDonor()
        {
            //create test characters and configure
            var speciminA = new Tank();
            var speciminB = new Tank();
            speciminA.updatePower(3);

            //create move instance and set target
            var move = new BloodDonor(speciminA);
            var targetAddResult = move.AddTarget(speciminB);

            //verify target was bound to move successfully
            Assert.IsTrue(targetAddResult == GameError.SUCCESS, $"Error binding target, result returned {targetAddResult}");

            //apply damage to specimin B
            speciminB.TakeDamage(50, true);

            //use move to heal speciminB
            move.ExecuteAction();

            //define expectations
            int speciminAHealth = 85;
            int speciminAPower = 8;
            int speciminBHealth = 60;
            int speciminBPower = 10;

            //verify results
            Assert.IsTrue(speciminA.Health == speciminAHealth, $"SpeciminA.Health expected:{speciminAHealth}, " +
                $"result:{speciminA.Health}");
            Assert.IsTrue(speciminA.Power == speciminAPower, $"SpeciminA.Power expected:{speciminAPower}, " +
                $"result:{speciminA.Power}");
            Assert.IsTrue(speciminB.Health == speciminBHealth, $"SpeciminA.Health expected:{speciminBHealth}, " +
                $"result:{speciminB.Health}");
            Assert.IsTrue(speciminB.Power == speciminBPower, $"SpeciminA.Power expected:{speciminBPower}, " +
                $"result:{speciminB.Power}");
        }
        /// <summary>
        /// Tests the tank's body slam move against another target
        /// </summary>
        [TestMethod]
        public void Test006_BodySlam()
        {
            var speciminA = new Tank();
            var speciminB = new Tank();
            speciminA.PartyID = 0;
            speciminB.PartyID = 1;
            speciminB.DefenseMultiplier = 0;

            var move = new BodySlam(speciminA);
            move.AddTarget(speciminB);
            move.ExecuteAction();

            int speciminAPower = 5;
            int speciminAHealth = 100;
            int speciminBPower = 10;
            int speciminBHealth = 88;

            //verify results
            Assert.IsTrue(speciminA.Health == speciminAHealth, $"SpeciminA.Health expected:{speciminAHealth}, " +
                $"result:{speciminA.Health}");
            Assert.IsTrue(speciminA.Power == speciminAPower, $"SpeciminA.Power expected:{speciminAPower}, " +
                $"result:{speciminA.Power}");
            Assert.IsTrue(speciminB.Health == speciminBHealth, $"SpeciminA.Health expected:{speciminBHealth}, " +
                $"result:{speciminB.Health}");
            Assert.IsTrue(speciminB.Power == speciminBPower, $"SpeciminA.Power expected:{speciminBPower}, " +
                $"result:{speciminB.Power}");
        }
        //[TestMethod]
        //public void Test007_ManOfSteelSpecial()
        //{ 

        //}
    }
}
