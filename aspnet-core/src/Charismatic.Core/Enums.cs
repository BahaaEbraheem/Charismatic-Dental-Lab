using Charismatic.Localization.SourceFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic
{
    public class Enums
    {

        public enum PersonType
        {

            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.SuperVisor))]
            SuperVisor = 1,

        }
        public enum ClosingStatus
        {

            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Agree))]
            Agree = 0,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Disagree))]
            Disagree,
        }
  
        public enum Status:byte        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Accepted))]

            Accepted = 0,
           [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Rejected))]

            Rejected = 1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Suspended))]

            Suspended = 2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.InProcess))]

            InProcess = 3,
        }


        public enum Gender
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Male))]
            Male = 1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Female))]
            Female = 0
        }


        public enum CaseInputSteps
        {
            PatientInfo,
            Evaluation,
            ProductsChoice
        }


        public enum CaseEvaluationType
        { /*
           * Item values are taken from requirments PDF page 2 (Case evalutation index)
           * Please don't change, they will be used to refer to pictures
           * */
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Complex))]
            Complex = 1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Simple))]
            Simple = 2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.DigitalWaxUp))]
            DigitalWaxUp = 3,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.ImplantFullMouth))]
            ImplantFullMouth = 4,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.CrownLengtheningGuide))]
            CrownLengtheningGuide = 5,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.ImplantSurgicalGuide))]
            ImplantSurgicalGuide = 6,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.TiMesh))]
            TiMesh = 7,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Ortho))]
            Ortho = 8,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Removable))]
            Removable = 9
        }

        public enum CaseType
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.PrivateCase))]
            Private =1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.CenterCase))]
            Center =2
        }

        public enum CaseStatus
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.New))]
            New =1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Submited))]
            Submited =2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Priced))]
            Priced =3,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.TpModification))]
            TpModification =4,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.TpAccepted))]
            TpAccepted =5,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.InProgress))]
            InProgress =6,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Delivery))]
            Delivery =7
        }

        public enum MissionStatus
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.New))]
            New = 1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.InProgress))]
            InProgress = 2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Rejected))]
            Rejected = 3,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Done))]
            Done = 4
        }
        public enum EmployeeStatus
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.New))]
            New = 1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.InProgress))]
            InProgress = 2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Done))]
            Done = 3
        }
        public enum CaseTypeWorkFlow
        {
            SimpleCaseWithImplantDigital=1
        }

        public enum CaseCreationStep
        {
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.PatientInfo))]
            PatientInfo =1,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Evaluation))]
            Evaluation =2,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Products))]
            Products =3,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Files))]
            Files =4,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.TeethTable))]
            TeethTable = 5,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.ColorsTable))]
            ColorsTable = 6,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Advanced1))]
            Advanced1 = 7,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Advanced2))]
            Advanced2 = 8,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Advanced3))]
            Advanced3 = 9,
            [Display(ResourceType = typeof(Tokens), Name = nameof(Tokens.Advanced4))]
            Advanced4 = 10,
        }

    }
}
