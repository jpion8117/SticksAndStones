using Microsoft.VisualStudio.TestTools.UnitTesting;
using SticksAndStones.Models.GameComponents;
using SticksAndStones.Models.GameComponents.Characters;
using SticksAndStones.Models.GameComponents.Moves;

namespace TankUnitTest
{
    [TestClass]
    public class TankHealthTests
    {
        /// <summary>
        /// Tests how Tank character reacts to direct damage (no defense multiplier applied) by attacking
        /// with 10hp straight attack then verifing result equals 90hp
        /// </summary>
        [TestMethod]
        public void Test001_DamageWithOutMultiplier()
        {
            Tank tank = new Tank();
            tank.TakeDamage(10, true);
            bool health90 = tank.Health == 90;
            Assert.IsTrue(health90, $"Tank health is reported as {tank.Health}HP but should be 90HP.");
        }

        /// <summary>
        /// Tests how damage multiplier applies to Tank character when standard damage is applied
        /// applies 10hp damage with standard multiplier of .5 then checks to make sure health 
        /// matches the expected 95hp result. Next changes multiplier to 0.8f (80% block) and
        /// attacks with 10hp standard damage then checks that damage matches the expected 
        /// result of 93HP (20% of 10 = 2, 95-2=93).
        /// </summary>
        [TestMethod]
        public void Test002_DamageWithMultiplier()
        {
            Tank tank = new Tank();
            tank.TakeDamage(10);
            Assert.IsTrue(tank.Health == 95, $"Tank health is reported as {tank.Health}HP but should be 95HP.");
            tank.DefenseMultiplier = .800000000000000000000000000000f;
            tank.TakeDamage(10);
            Assert.IsTrue(tank.Health == 93, $"Tank health is reported as {tank.Health}HP but should be 93HP.");
        }
        /// <summary>
        /// Tests how a negative damage multiplier is applied. If a character has a negative multiplier they
        /// will recieve more damage than the base damage. The test applies 10HP damage to Tank with a -1 
        /// defense multiplier (100% more damage) resulting in 20HP being removed.
        /// </summary>
        [TestMethod]
        public void Test003_DamageWithNegativeMultiplier()
        {
            var tank = new Tank(); 
            tank.DefenseMultiplier = -1;
            tank.TakeDamage(10);
            Assert.IsTrue(tank.Health == 80, $"tank health is reported as {tank.Health}HP but expected to be 80HP");
        }
        /// <summary>
        /// Tests how Tank character handles fatal attack. Expected result, tank is instanciated, takes 120hp direct
        /// damage, reports 0HP and IsAlive as false
        /// </summary>
        [TestMethod]
        public void Test004_DeathTest()
        {
            Tank tank = new Tank();
            tank.TakeDamage(120, true);
            Assert.IsTrue(tank.Health == 0, $"Health expected 0HP, Health reported {tank.Health}HP");
            Assert.IsTrue(!tank.IsAlive, $"Tank.IsAlive expected to be 'false', but returned '{tank.IsAlive}'");
        }
        /// <summary>
        /// Tests the character revival system. Characters can be revived only if recently killed. Upon revival the character
        /// will have 20% of their original HP. Test instanciates Tank character applies 120HP direct damage (no defense) to 
        /// wipe out character health, verifies character is dead, revives character by setting IsAlive to true, then 
        /// verifies player is alive and has 20% of total health.
        /// </summary>
        [TestMethod]
        public void Test005_RevivalTest()
        {
            var tank = new Tank();
            tank.TakeDamage(120, true);
            Assert.IsTrue(!tank.IsAlive, $"PRE-REVIVAL: Tank.IsAlive expected to be 'false', but returned '{tank.IsAlive}'");
            tank.IsAlive = true;
            Assert.IsTrue(tank.IsAlive, $"POST-REVIVAL: Tank.IsAlive expected to be 'true', but returned '{tank.IsAlive}'");
            Assert.IsTrue(tank.Health == 20, $"tank.health expected to be 20HP after revival, but returned {tank.Health}HP");
        }
        /// <summary>
        /// Tests the character revival system. Characters can be revived only if recently killed. Upon revival the character
        /// will have 20% of their original HP. Test instanciates Tank character applies 120HP direct damage (no defense) to 
        /// wipe out character health, verifies character is dead, simulates multiple life checks (these add decay), then 
        /// attempts revival, verifies that character is not revived and still has no health.
        /// </summary>
        [TestMethod]
        public void Test006_RevivalTestPostDecay()
        {
            var tank = new Tank();
            tank.TakeDamage(120, true);
            Assert.IsTrue(!tank.IsAlive, $"PRE-REVIVAL: Tank.IsAlive expected to be 'false', but returned '{tank.IsAlive}'");
            
            //simulate 3 rounds passing without being revived
            tank.ExecuteAction(ProcessMode.Round);
            tank.ExecuteAction(ProcessMode.Round);
            tank.ExecuteAction(ProcessMode.Round);
            
            tank.IsAlive = true;
            Assert.IsTrue(!tank.IsAlive, $"POST-REVIVAL: Tank.IsAlive expected to be 'false', but returned '{tank.IsAlive}'");

            Assert.IsTrue(tank.Health == 0, $"tank.health expected to be 0HP after revival attempt, but returned {tank.Health}HP");
        }
        [TestMethod]
        public void Test007_HealingTest1()
        {
            var tank = new Tank();
            tank.TakeDamage(50, true);
            Assert.IsTrue(tank.Health == 50, $"PRE HEALING: tank expected health was 50HP, but resulted was {tank.Health}");
            tank.updateHealth(20);
            Assert.IsTrue(tank.Health == 70, $"POST FIRST HEALING: tank expected health was 100HP, but resulted " +
                $"was {tank.Health}");
            tank.updateHealth(100);
            Assert.IsTrue(tank.Health == 100, $"POST SECOND HEALING: tank expected health was 100HP, but resulted " +
                $"was {tank.Health}");
        }
    }
}
