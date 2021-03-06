﻿using System;
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
                IQueryable<HorarioSemana> consultaHorarioSemanas = from d in bd.HorarioSemanaSet.Include("Dia").Include("Dia.Horario")
                                                             select d;
                return consultaHorarioSemanas.ToList() ;
            }
        }

        public void AgregarHorarioSemana(HorarioSemana miAgregarHorarioSemana)
        {
            CapaDeNegocios.blDia.blDia oblDia = new blDia.blDia();
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                foreach (Dia item in miAgregarHorarioSemana.Dia)
                {
                    if (item.Horario != null)
                    {
                        bd.HorarioSet.Attach(item.Horario);
                    }
                }
                bd.HorarioSemanaSet.Add(miAgregarHorarioSemana);
                bd.SaveChanges();
            }
        }

        public void ModificarHorarioSemana(HorarioSemana miModificarHorarioSemana)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                HorarioSemana auxiliar = (from c in bd.HorarioSemanaSet.Include("Dia").Include("Dia.HorarioDia") 
                                       where c.Id == miModificarHorarioSemana.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = miModificarHorarioSemana.Nombre;
                foreach (Dia item in auxiliar.Dia)
                {
                    foreach (Dia item2 in miModificarHorarioSemana.Dia)
                    {
                        if (item.NombreDiaSemana == item2.NombreDiaSemana )
                        {
                            item.Id = item2.Id;
                        }
                    }
                }
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
