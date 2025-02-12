using NUnit.Framework;
using System;

namespace ExperienceSystem.UnitTests
{
    [TestFixture]
    public class ExperienceTests
    {
        private const int DefaultStartLevel = 1;
        private const int MaxLevel = 5;
        private const float ExpPerLevel = 1000f;
        private const float SmallXP = 500f;
        private const float LevelUpXP = 1000f;
        private const float OverLevelXP = 5000f;
        private Experience _experience;

        [SetUp]
        public void SetUp()
        {
            _experience = new Experience(DefaultStartLevel, MaxLevel);
        }

        #region Constructor Tests

        [Test]
        public void Constructor_GivenStartLevel_SetsCorrectLevel()
        {
            Assert.That(_experience.Level, Is.EqualTo(DefaultStartLevel));
        }

        [Test]
        public void Constructor_GivenMaxLevel_SetsCorrectMaxLevel()
        {
            Assert.That(_experience.maxLevel, Is.EqualTo(MaxLevel));
        }

        #endregion

        #region AddXP Tests

        [Test]
        public void AddXP_GivenNegativeXP_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => _experience.AddXP(-100));
        }

        [Test]
        public void AddXP_GivenSmallAmount_IncreasesXPWithoutLevelingUp()
        {
            _experience.AddXP(SmallXP);
            Assert.That(_experience.exp, Is.EqualTo(SmallXP));
            Assert.That(_experience.Level, Is.EqualTo(DefaultStartLevel));
        }

        [Test]
        public void AddXP_GivenLevelUpAmount_IncreasesLevel()
        {
            bool wasCalled = false;
            _experience.OnLevelUp += () => wasCalled = true;
            _experience.AddXP(LevelUpXP);
            Assert.That(wasCalled);
            Assert.That(_experience.Level, Is.EqualTo(DefaultStartLevel + 1));
            Assert.That(_experience.exp, Is.EqualTo(0));
        }

        [Test]
        public void AddXP_GivenOverLevelXP_IncreasesMultipleLevels()
        {
            _experience.AddXP(OverLevelXP);
            Assert.That(_experience.Level, Is.EqualTo(MaxLevel));
            Assert.That(_experience.exp, Is.EqualTo(0));
        }

        [Test]
        public void AddXP_GivenXPAtMaxLevel_OnMaxLevelReachedIsCalled()
        {
            _experience.AddXP(OverLevelXP);
            bool wasCalled = false;
            _experience.OnMaxLevelReached += () => wasCalled = true;
            _experience.AddXP(LevelUpXP);
            Assert.That(wasCalled);
            Assert.That(_experience.Level, Is.EqualTo(MaxLevel));
            Assert.That(_experience.exp, Is.EqualTo(0));
        }

        #endregion

        #region ResetXP Tests

        [Test]
        public void ResetXP_SetsXPToZeroWithoutChangingLevel()
        {
            _experience.AddXP(SmallXP);
            _experience.ResetXP();
            Assert.That(_experience.exp, Is.EqualTo(0));
            Assert.That(_experience.Level, Is.EqualTo(DefaultStartLevel));
        }

        #endregion

        #region ResetProgression Tests

        [Test]
        public void ResetProgression_GivenNewLevel_ResetsLevelAndXP()
        {
            _experience.AddXP(OverLevelXP);
            _experience.ResetProgression(2);
            Assert.That(_experience.Level, Is.EqualTo(2));
            Assert.That(_experience.exp, Is.EqualTo(0));
        }

        [Test]
        public void ResetProgression_GivenDefaultValue_ResetsToStart()
        {
            _experience.AddXP(OverLevelXP);
            _experience.ResetProgression();
            Assert.That(_experience.Level, Is.EqualTo(DefaultStartLevel));
            Assert.That(_experience.exp, Is.EqualTo(0));
        }

        #endregion
    }
}