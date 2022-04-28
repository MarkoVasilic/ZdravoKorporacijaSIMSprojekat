using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoKorporacija.Model;
using ZdravoKorporacija.Service;

namespace ZdravoKorporacija.Controller
{
    public class PrescriptionController
    {
        private readonly PrescriptionService PrescriptionService;


        public PrescriptionController(PrescriptionService prescriptionService)
        {
            this.PrescriptionService = prescriptionService;
        }

        public PrescriptionController()
        {
        }

        public List<Prescription> GetAll()
        {
            return PrescriptionService.GetAll();
        }

        public Prescription? GetOneById(int prescriptionId)
        {
            return PrescriptionService.GetOneById(prescriptionId);
        }

        public void CreatePrescription(String patientJmbg, String medication, String amount, int frequency, DateTime from, DateTime to)
        {
                PrescriptionService.CreatePrescription(patientJmbg, medication, amount, frequency, from, to);

        }

        public void ModifyPrescription(int prescriptonId, String newMedication, String newAmount, int newFrequency, DateTime newFrom, DateTime newTo)
        {

            PrescriptionService.ModifyPrescription(prescriptonId, newMedication, newAmount, newFrequency, newFrom, newTo);

        }
    }
}
