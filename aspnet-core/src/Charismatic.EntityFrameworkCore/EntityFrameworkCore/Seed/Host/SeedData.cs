using Abp.MultiTenancy;
using Charismatic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Charismatic.Enums;
using CaseType = Charismatic.Models.CaseType;

namespace Charismatic.EntityFrameworkCore.Seed.Host
{
    public class SeedData
    {
        private readonly CharismaticDbContext _context;
        public SeedData(CharismaticDbContext context)
        {
            _context = context;
        }
        public void Create()
        {
            SeedDepartment();

            SeedCaseTypes();


            SeedWorkFlowTypes();
           SeedCase();
            //  SeedRoles();
            // SeedStaticCallTypes();
            // SeedUsers();
            // SeedUnits();
            // SeedChannels();
        }
        private void SeedCase() {
            var doctors = _context.Doctors.IgnoreQueryFilters().ToList();
            if (!doctors.Any())
            {
                doctors = new List<Doctor>()
                {
                      new Doctor()
                    {
                        UserId=1,
                        PhoneNumber="123",
                        ClinicPhone="123456",
                        ClinicEmail="BB@gmail.com",
                        ResponsipleName="Basel",
                        ResponsiplePhone="123456",
                        CreationTime=DateTime.Now,
                        IsDeleted=false
                    }
                };
                _context.Doctors.AddRange(doctors);
                _context.SaveChanges();

            }
            var patientReferrais = _context.PatientReferrais.IgnoreQueryFilters().ToList();
            if (!patientReferrais.Any())
            {
                patientReferrais = new List<PatientReferrais>()
                {
                      new PatientReferrais()
                    {
                        FirstName="ali",
                        LastName="Ahmad",
                        Email="BAB@gmail.com",
                        PhoneNumber="123456",
                        DateOfBirth=DateTime.Now,
                        StreetAddress="Test",
                        BuildingName="test",
                        Gender=Gender.Male,
                        CreationTime=DateTime.Now,
                        IsDeleted=false

                    }
                };
                _context.PatientReferrais.AddRange(patientReferrais);
                _context.SaveChanges();


            }
            var cases = _context.Cases.IgnoreQueryFilters().ToList();
            if (!cases.Any())
            {
                cases = new List<Case>()
                {
                      new Case()
                    {
                          Name="Test",
                          DoctorId=doctors.FirstOrDefault().Id,
                          PatientReferraisId=patientReferrais.FirstOrDefault().Id,
                          StartDate=DateTime.Now,
                          Type=Enums.CaseType.Private,
                          CaseStatus=CaseStatus.New,
                          CreationTime=DateTime.Now,
                          TypeId=_context.CaseTypes.Where(c=>c.Type==(byte)CaseTypeWorkFlow.SimpleCaseWithImplantDigital).FirstOrDefault().Id,
                          IsDeleted =false

                    }
                };
                _context.Cases.AddRange(cases);
                _context.SaveChanges();


            }

        }

        private void SeedCaseTypes()
        {
            var types = _context.CaseTypes.IgnoreQueryFilters()
               .Where(p => p.Type == (byte)CaseTypeWorkFlow.SimpleCaseWithImplantDigital

                 )
               .Select(p => p.Type)
               .ToList();
            if (!types.Any())
            {
                var typesList = new List<CaseType>()
                {
                      new CaseType()
                    {
                        Type=(byte)CaseTypeWorkFlow.SimpleCaseWithImplantDigital
                    }
                };

                _context.CaseTypes.AddRange(typesList);
                _context.SaveChanges();
            }


        }
        private void SeedWorkFlowTypes()
        {
            var workflow = _context.CaseTypeDepartmentWorkFlows.IgnoreQueryFilters()
               .Where(p => p.CaseTypeId == _context.CaseTypes.FirstOrDefault(x => x.Type == (byte)CaseTypeWorkFlow.SimpleCaseWithImplantDigital).Id)

               .Select(p => p.Id)
               .ToList();
            if (!workflow.Any())
            {
                var caseTypeId = _context.CaseTypes.FirstOrDefault(x => x.Type == (byte)CaseTypeWorkFlow.SimpleCaseWithImplantDigital).Id;
                var typesList = new List<CaseTypeDepartmentWorkFlow>()
                {
                    new CaseTypeDepartmentWorkFlow()
                    {
                        CaseTypeId=caseTypeId,
                        order=1,
                        DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="CAD").Id
                    },

                    new CaseTypeDepartmentWorkFlow()
                    {
                         CaseTypeId=caseTypeId,
                         order=2,
                         DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="CAM").Id
                    },

                    new CaseTypeDepartmentWorkFlow()
                    {
                        CaseTypeId=caseTypeId,
                        order=3,
                        DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Implant").Id
                    },

                     new CaseTypeDepartmentWorkFlow()
                     {
                            CaseTypeId=caseTypeId,
                            order=4,
                            DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Wax and Press").Id
                      },

                     new CaseTypeDepartmentWorkFlow()
                      {
                            CaseTypeId=caseTypeId,
                            order=5,
                            DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Fitting").Id
                      },

                    new CaseTypeDepartmentWorkFlow()
                    {
                        CaseTypeId=caseTypeId,
                        order=6,
                        DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Ceramic").Id
                    },

                    new CaseTypeDepartmentWorkFlow()
                    {
                        CaseTypeId=caseTypeId,
                        order=7,
                        DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Quality Control").Id
                    },


                    new CaseTypeDepartmentWorkFlow()

                            {

                                CaseTypeId=caseTypeId,
                                order=8,
                                DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Packing").Id
                            },

                    new CaseTypeDepartmentWorkFlow()
                                {
                                    CaseTypeId=caseTypeId,
                                    order=9,
                                    DepartmentId=_context.Departments.FirstOrDefault(x=>x.Name=="Delivery").Id
                                }





                };

                _context.CaseTypeDepartmentWorkFlows.AddRange(typesList);
                _context.SaveChanges();
            }


        }
        private void SeedDepartment()
        {
            var tenantId = CharismaticConsts.MultiTenancyEnabled ? null : (int?)MultiTenancyConsts.DefaultTenantId;
            var departmentforhost = _context.Departments.IgnoreQueryFilters()
                .Where(p => p.Name == "admin" || p.Name == "Plaster" || p.Name == "CAD" ||
                p.Name == "CAM" || p.Name == "Implant" ||
                 p.Name == "Wax" || p.Name == "Wax and Press" ||
                  p.Name == "Fitting" ||
                   p.Name == "Ceramic" || p.Name == "Polishing" ||
                   p.Name == "Quality Control" || p.Name == "Ortho" ||
                   p.Name == "Accounts" || p.Name == "Coordinator" ||
                     p.Name == "Delivery" || p.Name == "Packing"

                  )
                .Select(p => p.Name)
                .ToList();
            if (!departmentforhost.Any())
            {
                var departments = new List<Department>()
                {
                    new Department()
                    {
                        Name="admin"
                    },
                    new Department()
                    {
                        Name="Plaster"
                    },
                    new Department()
                    {
                        Name="CAD"
                    },
                    new Department()
                    {
                        Name="CAM"
                    },
                    new Department()
                    {
                        Name="Implant"
                    },
                      new Department()
                    {
                        Name="Wax"
                    },
                        new Department()
                    {
                        Name="Wax and Press"
                    },
                            new Department()
                    {
                        Name="Fitting"
                    },
                    new Department()
                    {
                        Name="Polishing"
                    },
                    new Department()
                    {
                        Name="Ceramic"
                    },
                    new Department()
                    {
                        Name="Quality Control"
                    },
                    new Department()
                    {
                        Name="Ortho"
                    },

                       new Department()
                    {
                        Name="Accounts"
                    },

                          new Department()
                    {
                        Name="Coordinator"
                          },

                          new Department()
                          {
                              Name="Delivery"
                          },
                          new Department()
                          {
                              Name="Packing"
                          }




                };
                _context.Departments.AddRange(departments);
                _context.SaveChanges();
            }
        }
    }
}
