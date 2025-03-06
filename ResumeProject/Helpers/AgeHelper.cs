using System;

namespace ResumeProject.Helpers;

public class AgeHelper
{
    public int CalculateAge(DateTime dateOfBirth)
    {
        var age = DateTime.Now.Year - dateOfBirth.Year;

        if(DateTime.Now.Date < dateOfBirth.Date.AddYears(age))
        {
            
            age--;
        }

        return age;
    }
}
