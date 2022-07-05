using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarvalTestCSVReader.Models.Core
{
    public class PersonRow : ValidatedRow
    {
        private readonly PersonRowConfig _config;
        const int INDEX_OF_IDENTITY = 0;
        const int INDEX_OF_FIRST_NAME = 1;
        const int INDEX_OF_SURNAME = 2;
        const int INDEX_OF_AGE = 3;
        const int INDEX_OF_SEX = 4;
        const int INDEX_OF_MOBILE = 5;
        const int INDEX_OF_ACTIVE = 6;

        public PersonRow() { }

        public PersonRow(Row row, PersonRowConfig config)
        {
            _config = config;

            this.Row = row.Index;

            SetupFields(row.Columns);
        }

        private void SetupFields(List<Column> columns)
        {
            var errors = new List<string>();

            Identity = GetColumnValue(columns, INDEX_OF_IDENTITY, "").Trim();
            FirstName = GetColumnValue(columns, INDEX_OF_FIRST_NAME, "").Trim();
            Surname = GetColumnValue(columns, INDEX_OF_SURNAME, "").Trim();

            var ageSupplied = GetColumnValue(columns, INDEX_OF_AGE, "").Trim();

            if (_config.IsAgeRequired && string.IsNullOrWhiteSpace(ageSupplied))
                errors.Add($"{nameof(Age)} not specified");

            if (!string.IsNullOrWhiteSpace(ageSupplied)) 
            { 
                if (!(int.TryParse(ageSupplied, out int _age)) && ageSupplied.Length > 4)
                {
                    errors.Add($"{nameof(Age)} must be a valid age. Provided age: {ageSupplied} is invalid");
                }
                Age = ageSupplied;
            }

            var sexSupplied = GetColumnValue(columns, INDEX_OF_SEX, "").Trim();

            if (_config.IsSexRequired && string.IsNullOrWhiteSpace(sexSupplied))
                errors.Add($"{nameof(Sex)} not specified");

            if (string.IsNullOrWhiteSpace(sexSupplied))
            {
                var c = sexSupplied[0].ToString();

                if (sexSupplied.Length > 1
                    && !(c.Equals("m", StringComparison.InvariantCultureIgnoreCase)
                    || c.Equals("f", StringComparison.InvariantCultureIgnoreCase)))
                    errors.Add($"{nameof(Sex)} must be either 'M' or 'F'. Provided sex: {sexSupplied} is invalid");

                Sex = sexSupplied;
            }

            var providedMobile = GetColumnValue(columns, INDEX_OF_MOBILE, "");

            if (_config.IsPhoneNumberRequired && string.IsNullOrWhiteSpace(providedMobile))
                errors.Add($"{nameof(Mobile)} not specified");

            if (!string.IsNullOrWhiteSpace(providedMobile))
            {
                if (!decimal.TryParse(providedMobile, out decimal _mobile))
                {
                    errors.Add($"{nameof(Mobile)} must be number. Provided mobile number: {providedMobile} is invalid");
                }
                Mobile = _mobile;
            }

            var suppliedActive = GetColumnValue(columns, INDEX_OF_ACTIVE, "");

            if (_config.IsActiveRequired && string.IsNullOrWhiteSpace(suppliedActive))
                errors.Add($"{nameof(Active)} not specified");

            if (!string.IsNullOrWhiteSpace(suppliedActive))
            {
                if (!bool.TryParse(suppliedActive, out bool active))
                {
                    errors.Add($"{nameof(Active)} must be number. Provided sex: {suppliedActive} is invalid");
                }
                Active = active;
            }

            if (string.IsNullOrWhiteSpace(Identity))
                errors.Add($"{nameof(Identity)} not specified");
            if (string.IsNullOrWhiteSpace(FirstName))
                errors.Add($"{nameof(FirstName)} not specified");
            if (string.IsNullOrWhiteSpace(Surname))
                errors.Add($"{nameof(Surname)} not specified");

            IsValid = errors.Count == 0;
            if (!IsValid) ErrorMessages = errors;
        }

        public string Identity { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public decimal Mobile { get; set; }
        public bool Active { get; set; }

    }

}
