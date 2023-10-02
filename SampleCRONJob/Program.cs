

using System.Globalization;

string filePath = "Data/ingest-me.csv";

Console.WriteLine($"Job started @ {DateTime.Now}");


if (!File.Exists(filePath))
{
    Console.WriteLine($"{filePath} does not exist.");
    return;
}

List<Person> people = new();

try
{
    Console.WriteLine($"Reading contents of {filePath} ...");

    foreach (var line in File.ReadLines(filePath))
    {
        var columns = line.Split(',');

        if (columns.Length == 4)
        {
            if (int.TryParse(columns[0], out int id) &&
                DateTime.TryParseExact(columns[3], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dob))
            {
                var person = new Person() {
                    Id = id,
                    FirstName = columns[1],
                    Surname = columns[2],
                    DateOfBirth = dob
                };

                people.Add(person);
            }
        }
    }

    // Display the parsed people
    foreach (var person in people)
    {
        Console.WriteLine($"ID: {person.Id}, Name: {person.FirstName} {person.Surname}, Date of Birth: {person.DateOfBirth.ToShortDateString()}, Age: {person.Age}");
    }

    Console.WriteLine($"Job finshed @ {DateTime.Now}");
}
catch (IOException e)
{
    Console.WriteLine("An error occurred while reading the file: " + e.Message);
}

 public class Person {

    public int Id {get;set;}

    public string FirstName {get;set;}

    public string Surname {get;set;}

    public DateTime DateOfBirth {get;set;}

    public int Age {
        get
        {
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - DateOfBirth.Year;
        
            if (currentDate.Month < DateOfBirth.Month || (currentDate.Month == DateOfBirth.Month && currentDate.Day < DateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
  
}
