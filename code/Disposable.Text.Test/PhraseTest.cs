using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Disposable.Text.Test
{
    [TestClass]
    public class PhraseTest
    {
        [TestMethod]
        public void ValidConstruction_WhenGettingValueUpperLowerLength_ReturnsExpected()
        {
            // Arrange
            var value = "caSey GoodHew";
            var upper = "CASEY GOODHEW";
            var lower = "casey goodhew";
            var length = 13;

            // Act
            var phrase = new Phrase(value);

            // Assert
            Assert.AreEqual(value, phrase.Value);
            Assert.AreEqual(upper, phrase.Upper);
            Assert.AreEqual(lower, phrase.Lower);
            Assert.AreEqual(length, phrase.Length);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithEmptyValue_Throws()
        {
            new Phrase(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithNullValue_Throws()
        {
            new Phrase(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidConstruction_WithWhitespaceValue_Throws()
        {
            new Phrase(" ");
        }

        [TestMethod]
        public void ConstructPhraseFromUnderscoreDelimited_AsEachDelimiter_Succeeds()
        {
            var value = "casey_GOODHEW_was_Here";
            
            var phrase = new Phrase(value, SplitStyle.UnderscoreDelimited);

            Assert.AreEqual("caseyGoodhewWasHere", phrase.As(JoinStyle.LowerCamelCase).Value);
            Assert.AreEqual("CaseyGoodhewWasHere", phrase.As(JoinStyle.UpperCamelCase).Value);

            Assert.AreEqual("casey GOODHEW was Here", phrase.As(JoinStyle.SpaceDelimited).Value);
            Assert.AreEqual("casey goodhew was here", phrase.As(JoinStyle.LowerSpaceDelimited).Value);
            Assert.AreEqual("CASEY GOODHEW WAS HERE", phrase.As(JoinStyle.UpperSpaceDelimited).Value);
            Assert.AreEqual("Casey Goodhew Was Here", phrase.As(JoinStyle.ProperSpaceDelimited).Value);

            Assert.AreEqual("casey-GOODHEW-was-Here", phrase.As(JoinStyle.DashDelimited).Value);
            Assert.AreEqual("casey-goodhew-was-here", phrase.As(JoinStyle.LowerDashDelimited).Value);
            Assert.AreEqual("CASEY-GOODHEW-WAS-HERE", phrase.As(JoinStyle.UpperDashDelimited).Value);
            Assert.AreEqual("Casey-Goodhew-Was-Here", phrase.As(JoinStyle.ProperDashDelimited).Value);

            Assert.AreEqual("casey_GOODHEW_was_Here", phrase.As(JoinStyle.UnderscoreDelimited).Value);
            Assert.AreEqual("casey_goodhew_was_here", phrase.As(JoinStyle.LowerUnderscoreDelimited).Value);
            Assert.AreEqual("CASEY_GOODHEW_WAS_HERE", phrase.As(JoinStyle.UpperUnderscoreDelimited).Value);
            Assert.AreEqual("Casey_Goodhew_Was_Here", phrase.As(JoinStyle.ProperUnderscoreDelimited).Value);
        }

        [TestMethod]
        public void ConstructSingleWordFromUnderscoreDelimited_AsEachDelimiter_Succeeds()
        {
            var value = "caSey";

            var phrase = new Phrase(value, SplitStyle.UnderscoreDelimited);

            Assert.AreEqual("casey", phrase.As(JoinStyle.LowerCamelCase).Value);
            Assert.AreEqual("Casey", phrase.As(JoinStyle.UpperCamelCase).Value);

            Assert.AreEqual("caSey", phrase.As(JoinStyle.SpaceDelimited).Value);
            Assert.AreEqual("casey", phrase.As(JoinStyle.LowerSpaceDelimited).Value);
            Assert.AreEqual("CASEY", phrase.As(JoinStyle.UpperSpaceDelimited).Value);
            Assert.AreEqual("Casey", phrase.As(JoinStyle.ProperSpaceDelimited).Value);

            Assert.AreEqual("caSey", phrase.As(JoinStyle.DashDelimited).Value);
            Assert.AreEqual("casey", phrase.As(JoinStyle.LowerDashDelimited).Value);
            Assert.AreEqual("CASEY", phrase.As(JoinStyle.UpperDashDelimited).Value);
            Assert.AreEqual("Casey", phrase.As(JoinStyle.ProperDashDelimited).Value);

            Assert.AreEqual("caSey", phrase.As(JoinStyle.UnderscoreDelimited).Value);
            Assert.AreEqual("casey", phrase.As(JoinStyle.LowerUnderscoreDelimited).Value);
            Assert.AreEqual("CASEY", phrase.As(JoinStyle.UpperUnderscoreDelimited).Value);
            Assert.AreEqual("Casey", phrase.As(JoinStyle.ProperUnderscoreDelimited).Value);
        }

        [TestMethod]
        public void ConstructAsEachDelimited_AsProperSpaceDelimited_Succeeds()
        {
            var tests = new Dictionary<string, string>();
            tests["auto"] = "auto";
            tests["lowerCamelCase"] = "lower Camel Case";
            tests["UpperCamelCase"] = "Upper Camel Case";
            tests["Space Delimited"] = "Space Delimited";
            tests["Dash-Delimited"] = "Dash Delimited";
            tests["underscore_delimited"] = "underscore delimited";
            tests["CombinationOf Every-thing_we HAve"] = "Combination Of Every thing we H Ave";

            foreach (var kvp in tests)
            {
                var phrase = new Phrase(kvp.Key);
                Assert.AreEqual(kvp.Value, phrase.As(JoinStyle.SpaceDelimited).Value);
            }
        }
    }
}
