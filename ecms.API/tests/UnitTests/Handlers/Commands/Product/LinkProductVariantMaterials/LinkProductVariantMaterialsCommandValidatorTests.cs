using ecms.Application.Handlers.Commands.LinkProductVariantMaterials;
using ecms.Application.Models.Dtos.Materials;
using ecms.Domain.Errors.ProductMaterials;
using FluentValidation.TestHelper;

namespace UnitTests.Handlers.Commands.Product.LinkProductVariantMaterials
{
    public class LinkProductVariantMaterialsCommandValidatorTests
    {
        private readonly LinkProductVariantMaterialsCommandValidator _validator;

        public LinkProductVariantMaterialsCommandValidatorTests()
        {
            _validator = new LinkProductVariantMaterialsCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_ProductVariantId_Is_Empty()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 0,
                ProductMaterialDtos = new List<ProductMaterialDto>()
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductVariantId)
                .WithErrorCode(ProductMaterialErrorCodes.MissingProductVariantId);
        }

        [Fact]
        public void Should_Have_Error_When_ProductMaterialDtos_Is_Empty()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 1,
                ProductMaterialDtos = new List<ProductMaterialDto>()
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductMaterialDtos)
                .WithErrorCode(ProductMaterialErrorCodes.InvalidProductMaterialsAmount);
        }

        [Fact]
        public void Should_Have_Error_When_MaterialId_Is_Empty()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 1,
                ProductMaterialDtos = new List<ProductMaterialDto>
                {
                    new ProductMaterialDto
                    {
                        MaterialId = 0,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("ProductMaterialDtos[0].MaterialId")
                .WithErrorCode(ProductMaterialErrorCodes.MissingMaterialId);
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_Empty()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 1,
                ProductMaterialDtos = new List<ProductMaterialDto>
                {
                    new ProductMaterialDto
                    {
                        MaterialId = 1,
                        Quantity = 0
                    }
                }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("ProductMaterialDtos[0].Quantity")
                .WithErrorCode(ProductMaterialErrorCodes.MissingQuantity);
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_Negative()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 1,
                ProductMaterialDtos = new List<ProductMaterialDto>
                {
                    new ProductMaterialDto
                    {
                        MaterialId = 1,
                        Quantity = -1
                    }
                }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor("ProductMaterialDtos[0].Quantity")
                .WithErrorCode(ProductMaterialErrorCodes.InvalidQuantity);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var command = new LinkProductVariantMaterialsCommand
            {
                ProductVariantId = 1,
                ProductMaterialDtos = new List<ProductMaterialDto>
                {
                    new ProductMaterialDto
                    {
                        MaterialId = 1,
                        Quantity = 1
                    }
                }
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}