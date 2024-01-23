using H_Sports.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;



namespace H_Sports.Interfaces



{
    

    public interface ISportsRepository
    {
        List<Sport> GetSports();

        Sport GetSportBySportName (string sportName);
        
    }
}
