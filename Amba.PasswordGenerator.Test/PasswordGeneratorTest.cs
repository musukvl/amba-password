using System.Linq;
using System.Text.RegularExpressions;
using McMaster.Extensions.CommandLineUtils;
using NUnit.Framework;

namespace Amba.PasswordGenerator.Test
{
    public class PasswordGeneratorTest
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(8)]
        [TestCase(16)]
        public void BasicScenario(int length)
        {
            var passwordGenerator = new PasswordGeneratorCommand(PhysicalConsole.Singleton);
            var result = passwordGenerator.Generate(length);
            
            TestContext.Out.WriteLine($"Generated password {result}" );
            Assert.AreEqual(length, result.Length, "Wrong password length");
            Assert.IsTrue(Regex.IsMatch(result, @"\d"), "Password has no numbers");
            Assert.IsTrue(Regex.IsMatch(result, @"[a-zA-Z]"), "Password has no letters");
            Assert.IsTrue(Regex.IsMatch(result, @"[-_{}@!()\[\]|']"), "Password has no symbols");
        }
        
        [Test]
        [TestCase(3)]
        [TestCase(16)]
        public void GenerateNoSymbols(int length)
        {
            var passwordGenerator = new PasswordGeneratorCommand(PhysicalConsole.Singleton);
            var result = passwordGenerator.Generate(length, addSymbols: false);
            
            TestContext.Out.WriteLine($"Generated password {result}" );
            Assert.AreEqual(length, result.Length, "Wrong password length");
            Assert.IsTrue(Regex.IsMatch(result, @"\d"), "Password has no numbers");
            Assert.IsTrue(Regex.IsMatch(result, @"[a-zA-Z]"), "Password has no letters");
            Assert.IsTrue(!Regex.IsMatch(result, @"[-_{}@!()\[\]|']"), "Password has symbols");
        }
        
        [Test]
        [TestCase(1)] 
        public void EdgeCase(int length)
        {
            var passwordGenerator = new PasswordGeneratorCommand(PhysicalConsole.Singleton);
            var result = passwordGenerator.Generate(length);
            
            TestContext.Out.WriteLine($"Generated password {result}" );
            Assert.AreEqual(length, result.Length, "Wrong password length");
        }
    }
}