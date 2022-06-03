using System.Collections.Generic;
using Newtonsoft.Json;
using ZdravoKorporacija.Model;

namespace ZdravoKorporacija.Interfaces;

public interface IAdvancedRenovationSeparationRepository
{
    public List<AdvancedRenovationSeparation> FindAll();
    public void SaveSeparation(AdvancedRenovationSeparation renovationToMake);
    public void RemoveSeparation(int renovationId);
    public AdvancedRenovationSeparation? FindOneById(int? id);
    public List<AdvancedRenovationSeparation> FindAllByRoomId(int id);
}