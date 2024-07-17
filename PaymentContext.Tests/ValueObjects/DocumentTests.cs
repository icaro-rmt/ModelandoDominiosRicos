
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        //Red,Green,Refactoring
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsFalse(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var doc = new Document("94312367000194", EDocumentType.CNPJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {

            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsFalse(doc.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("82811178007")]
        [DataRow("77584571000")]
        [DataRow("48642064064")]
        [DataRow("06420433029")]
        public void ShouldReturnSuccessWhenCPFIsValid(string cpf)
        {

            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }

    }
}