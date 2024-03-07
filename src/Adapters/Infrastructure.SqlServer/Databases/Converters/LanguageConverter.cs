using Domain.ValueTypes;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.SqlServer.Databases.Converters;

public class LanguageConverter : ValueConverter<Language, string>
{
	public LanguageConverter()
		: base(
			valueType => valueType.IsoCode,
			isoCode => (Language)isoCode) { }
}
