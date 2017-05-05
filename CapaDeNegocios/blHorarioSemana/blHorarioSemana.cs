using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blHorarioSemana
{
    public class blHorarioSemana
    {
        public ICollection<HorarioSemana> ListarHorarioSemanas()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<HorarioSemana> consultaHorarioSemanas = from d in bd.HorarioSemanaSet
                                                             select d;
                return consultaHorarioSemanas.ToList() ;
            }
        }

        public void AgregarHorarioSemana(HorarioSemana miAgregarHorarioSemana)
        {
            CapaDeNegocios.blDia.blDia oblDia = new blDia.blDia();

           

            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioDia auxHorarioDia;
                foreach (Dia item in miAgregarHorarioSemana.Dia)
                {
                    if (item.HorarioDia != null)
                    {
                        //auxHorarioDia = bd.HorarioDiaSet.Find(item.HorarioDia.Id);
                        //bool isDetached = bd.Entry(auxHorarioDia).State == System.Data.Entity.EntityState.Detached ;
                        //if (isDetached)
                        //{
                            bd.HorarioDiaSet.Attach(item.HorarioDia); //  .Entry(item.HorarioDia).State = System.Data.Entity.EntityState.Unchanged;
                        //}
                        
                        //HorarioDia auxDia = bd.HorarioDiaSet.Find(item.HorarioDia.Id);

                        //if (auxDia == null)
                        //{
                        //    bd.HorarioDiaSet.Attach(item.HorarioDia);
                        //}
                    }
                   
                    
                }

                bd.HorarioSemanaSet.Add(miAgregarHorarioSemana);
                
                bd.SaveChanges();
            }
            //foreach (Dia item in miAgregarHorarioSemana.Dia)
            //{
            //    oblDia.AgregarDia(item);
            //}
        }

        public void ModificarHorarioSemana(HorarioSemana miModificarHorarioSemana)
        {
            
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioSemana auxiliar = (from c in bd.HorarioSemanaSet
                                       where c.Id == miModificarHorarioSemana.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarHorarioSemana.Nombre;
                bd.SaveChanges();
            }
        }

        public void EliminarHorarioSemana(HorarioSemana miEliminarHorarioSemana)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioSemana auxiliar = (from c in bd.HorarioSemanaSet
                                       where c.Id == miEliminarHorarioSemana.Id
                                       select c).FirstOrDefault();
                bd.HorarioSemanaSet.Remove(auxiliar);
            }
        }
    }
}
