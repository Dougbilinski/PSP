using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Pims.Ltsa.Models;
using System.IO;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

namespace Pims.Dal.Test.Libraries.Ltsa
{
    [Trait("category", "unit")]
    [Trait("category", "ltsa")]
    [Trait("group", "ltsa")]
    [ExcludeFromCodeCoverage]
    public class StcOrderTest
    {
        [Fact]
        public void TestConstructor()
        {
            StateTitleCertificateOrderParameters stateTitleCertificateOrderParameters = new StateTitleCertificateOrderParameters(titleNumber: "titleNumber");
            StcOrder obj = new StcOrder(stateTitleCertificateOrderParameters);
            obj.ProductOrderParameters.Should().Be(stateTitleCertificateOrderParameters);
        }
    }
}