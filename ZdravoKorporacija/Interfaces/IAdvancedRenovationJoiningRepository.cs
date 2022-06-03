using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ZdravoKorporacija.Model;
using Model;

namespace ZdravoKorporacija.Interfaces;

public interface IAdvancedRenovationJoiningRepository
{
    public List<AdvancedRenovationJoining> FindAll();
    public void SaveJoining(AdvancedRenovationJoining renovationToMake);
    public void RemoveJoining(int renovationId);
    public AdvancedRenovationJoining? FindOneById(int? id);
    public List<AdvancedRenovationJoining> FindAllByRoomId(int id);
}