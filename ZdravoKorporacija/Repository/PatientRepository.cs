using System;

namespace Repository
{
   public class PatientRepository
   {
      private String patientFilePath;
      
      public List<Patient> FindAll()
      {
         throw new NotImplementedException();
      }
      
      public Model.Patient SavePatient(Model.Patient PacientToMake)
      {
         throw new NotImplementedException();
      }
      
      public Boolean RemovePatient(long Jmbg)
      {
         throw new NotImplementedException();
      }
      
      public Model.Patient FindOneByJmbg(long Jmbg)
      {
         throw new NotImplementedException();
      }
   
   }
}