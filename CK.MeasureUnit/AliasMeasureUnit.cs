using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CK.Core
{
    /// <summary>
    /// An <see cref="AliasMeasureUnit"/> is a <see cref="AtomicMeasureUnit"/> that has its own unique abbreviation and name,
    /// is defined by a <see cref="FullFactor"/> applied to a <see cref="MeasureUnit"/>. 
    /// </summary>
    public sealed class AliasMeasureUnit : AtomicMeasureUnit
    {
        internal AliasMeasureUnit(
            string abbreviation, string name,
            FullFactor definitionFactor,
            MeasureUnit definition )
            : base( abbreviation, name, false )
        {
            DefinitionFactor = definitionFactor;
            Definition = definition;
        }

        /// <summary>
        /// Gets the definition factor.
        /// </summary>
        public FullFactor DefinitionFactor { get; }

        /// <summary>
        /// Gets the unit definition.
        /// </summary>
        public MeasureUnit Definition { get; }

        private protected override (MeasureUnit, FullFactor) GetNormalization()
        {
            return (Definition.Normalization, Definition.NormalizationFactor.Multiply( DefinitionFactor ));
        }


    }
}
