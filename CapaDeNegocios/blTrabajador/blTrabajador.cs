﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntities;
using CapaDeDatos;

namespace CapaDeNegocios.blTrabajador
{
    public class blTrabajador
    {
        public ICollection<Trabajador> ListaTrabajadores(Local miLocal)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Trabajador> consultaTrabajadores = from d in bd.TrabajadorSet
                                                              where d.OficinaActual.Local.Id == miLocal.Id
                                                              select d;
                return consultaTrabajadores.ToList();
            }
        }

        public ICollection<Trabajador> ListaTrabajadores(Oficina miOficina)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Trabajador> consultaTrabajadores = from d in bd.TrabajadorSet
                                                              where d.OficinaActual.Id  == miOficina.Id
                                                              select d;
                return consultaTrabajadores.ToList();
            }
        }

        public ICollection<Trabajador> ListaTrabajadores()
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                IQueryable<Trabajador> consultaTrabajadores = from d in bd.TrabajadorSet
                                                              select d;
                return consultaTrabajadores.ToList();
            }
        }

        public void AgregarTrabajador(Trabajador miNuevoTrabajador)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                bd.OficinaSet.Attach(miNuevoTrabajador.OficinaActual);
                bd.TrabajadorSet.Add(miNuevoTrabajador);
                bd.SaveChanges();
            }
        }

        public void ModificarTrabajador(Trabajador trabajadorAModificar)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Trabajador auxiliar = (from c in bd.TrabajadorSet
                                       where c.Id == trabajadorAModificar.Id
                                       select c).FirstOrDefault();
                auxiliar.Nombre = trabajadorAModificar.Nombre;
                auxiliar.ApellidoPaterno = trabajadorAModificar.ApellidoPaterno;
                auxiliar.ApellidoMaterno = trabajadorAModificar.ApellidoMaterno;
                auxiliar.DNI = trabajadorAModificar.DNI;
                bd.SaveChanges();
            }
        }

        public void EliminarTrabajador (Trabajador trabajadorAEliminar)
        {
            using (mAsistenciaContainer bd = new mAsistenciaContainer())
            {
                Trabajador auxiliar = (from c in bd.TrabajadorSet
                                       where c.Id == trabajadorAEliminar.Id
                                       select c).FirstOrDefault();
                bd.TrabajadorSet.Remove(auxiliar);
            }
        }
    }
}
