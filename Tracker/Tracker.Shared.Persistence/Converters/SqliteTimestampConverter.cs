using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tracker.Shared.Persistence.Converters
{
    // Source: https://stackoverflow.com/questions/52684458/updating-entity-in-ef-core-application-with-sqlite-gives-dbupdateconcurrencyexce
    public class SqliteTimestampConverter : ValueConverter<byte[], string>
    {
        public SqliteTimestampConverter() : base(
            v => v == null ? null : ToDb(v),
            v => v == null ? null : FromDb(v)
        )
        { }

        static byte[] FromDb(string v) =>
            v.Select(c => (byte) c).ToArray(); // Encoding.ASCII.GetString(v)

        static string ToDb(byte[] v) => 
            new(v.Select(b => (char) b).ToArray()); // Encoding.ASCII.GetBytes(v))
    }
}