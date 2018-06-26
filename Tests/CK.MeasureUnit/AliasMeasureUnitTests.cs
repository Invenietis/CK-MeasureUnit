using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CK.Core.MeasureUnit;

namespace CK.Core.Tests
{
    [TestFixture]
    public class AliasMeasureUnitTests
    {
        [Test]
        public void an_alias_can_not_be_redefined_differently()
        {
            var dm3 = MeasureStandardPrefix.Deci[MeasureUnit.Metre] ^ 3;
            var litre = MeasureUnit.DefineAlias( "l", "Litre", FullFactor.Neutral, dm3 );
            litre.Should().NotBeSameAs( dm3 );
            litre.Abbreviation.Should().Be( "l" );
            litre.Name.Should().Be( "Litre" );

            Action a;
            a = () => MeasureUnit.DefineAlias( "l", "litre", FullFactor.Neutral, dm3 );
            a.Should().Throw<Exception>( "Case sensitivity of name." );

            a = () => MeasureUnit.DefineAlias( "l", "Litre", new FullFactor( 1.1, ExpFactor.Neutral ), dm3 );
            a.Should().Throw<Exception>( "Different factor." );

            a = () => MeasureUnit.DefineAlias( "l", "Litre", FullFactor.Neutral, dm3 / MeasureUnit.Metre );
            a.Should().Throw<Exception>( "Different definition." );

            var litre2 = MeasureUnit.DefineAlias( "l", "Litre", FullFactor.Neutral, dm3 );
            litre2.Should().BeSameAs( litre );
        }


        [Test]
        public void defining_actual_units()
        {
            /*
            -	A newton (N): its definition directly uses fundamental units: 1 N = 1 kg.m.s-2
            -	A dyne (dyn) is defined with the newton: 1 dyn	 = 10-5 N
            -	A kilopond (kp) is: 1 kp	= 9.80665 N 
            */
            var kg = Kilogram;
            var m = Metre;
            var s = Second;

            var newton = DefineAlias( "N", "Newton", FullFactor.Neutral, kg * m * (s ^ -2) );
            var dyne = DefineAlias( "dyn", "Dyne", new ExpFactor( 0, -5 ), newton );
            var kilopound = DefineAlias( "kp", "Kilopound", 9.80665, newton );

            var newtonPerDyne = newton / dyne;
            newtonPerDyne.Abbreviation.Should().Be( "N.dyn-1" );
        }
    }
}

