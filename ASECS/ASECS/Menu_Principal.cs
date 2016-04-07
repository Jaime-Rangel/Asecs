﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ASECS
{
    public partial class Menu_Principal : Form
    {
        //Objetos
        Menu_Principal_Aspectos Aspectos;
        public Variables_Menu_Principal Variables_Globales;
        public Arbol_Camara<Camara> Lista_Camaras;
        public List<string> Lista_Camaras_Alias;
        Camara Busqueda_Objeto_Camara;
        Menu_Principal_Metodos Metodos;
        public Tiempo Objeto_Tiempo;

        public Menu_Principal()
        {
            InitializeComponent();
        }

        private void Menu_Principal_Load(object sender, EventArgs e)
        {
            Inicializar_Objetos();
            Inicializar_Variables_Camara();
            Aspectos.Acomodar_Elementos();
            Aspectos.Asignar_Mensaje_Bienvenida();

            Icono_Notificacion.ShowBalloonTip(5000, "Welcome", "Hello ", ToolTipIcon.Info);
        }

        public void Inicializar_Objetos()
        {
            Variables_Globales = new Variables_Menu_Principal();
            Aspectos = new Menu_Principal_Aspectos(this, Variables_Globales);
            Lista_Camaras_Alias = new List<string>();
            Metodos = new Menu_Principal_Metodos(this);
            Objeto_Tiempo = new Tiempo();

            Metodos.Asignar_Parametros_Icono_Notificacion();

            Lista_Camaras = new Arbol_Camara<Camara>((Camara a, Camara b) =>
            {
                return string.Compare(a.Alias, b.Alias) < 0;

            }, (Camara Objeto) =>
            {
                return Objeto;

            },(Camara a,Camara b) =>
            {
                if (a.Alias.Equals(b.Alias))
                {
                    return true;
                }
                else
                    return false;
            });
        }

        public int Eliminar_Nodos_Camaras(Camara Eliminar_Camara)
        {
            int index;
            Lista_Camaras.remove(Eliminar_Camara);

            index = Lista_Camaras_Alias.IndexOf(Eliminar_Camara.Alias);
            Lista_Camaras_Alias.RemoveAt(index);

            return index;
        }

        public void Crear_Nodos_Camaras(Camara Nueva_Camara)
        {
            Lista_Camaras_Alias.Add(Nueva_Camara.Alias);
            Lista_Camaras.Insertar(Nueva_Camara);
        }

        public bool Buscar_Camaras_Alias_Nodos(string Alias)
        {
            //Busca una camara en el arbol de camaras
            Busqueda_Objeto_Camara = new Camara();
            Camara Resultado_Busqueda_Objeto_Camara_Alias = new Camara();

            Busqueda_Objeto_Camara.Alias = Alias;

            Resultado_Busqueda_Objeto_Camara_Alias = Lista_Camaras.Buscar(Busqueda_Objeto_Camara);

            if (Resultado_Busqueda_Objeto_Camara_Alias == null)
            {
                return false;
            }
            else
                return true;
            
        }

        public void Inicializar_Variables_Camara()
        {
            Variables_Globales.Nombre_Usuario = "Jaime Rangel Ojeda";
        }

        private void Menu_Principal_Nueva_Camara_Click(object sender, EventArgs e)
        {
            Menu_Nueva_Camara Agregar_Camara = new Menu_Nueva_Camara(this);
            Agregar_Camara.Show();

        }

        private void Menu_Principal_Cambiar_Rutas_Click(object sender, EventArgs e)
        {
            string folder;
            DialogResult result = Dialogo_Ruta_Grabacion.ShowDialog();
            if( result == DialogResult.OK )
            {
                folder = Dialogo_Ruta_Grabacion.SelectedPath;
                Variables_Globales.Ruta_Grabacion = folder;
                MessageBox.Show("La ruta se ha almacenado correctamente");
            }
        }

        private void Menu_Principal_Administrar_Camara_Click(object sender, EventArgs e)
        {
            Menu_Administracion_Camara Administrar_Camara = new Menu_Administracion_Camara(this);
            Administrar_Camara.Show();
        }

        private void Menu_Principal_Eliminar_Camara_Click(object sender, EventArgs e)
        {
            Menu_Eliminar_Camara Eliminar_Camara = new Menu_Eliminar_Camara(this);
            Eliminar_Camara.Show();
        }

        private void Menu_Principal_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                Icono_Notificacion.Visible = true;
                Icono_Notificacion.ShowBalloonTip(2500);
            }
        }

        private void Icono_Notificacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            Icono_Notificacion.Visible = false;  
        }

        private void Icono_Notificacion_BalloonTipShown(object sender, EventArgs e)
        {
            Icono_Notificacion.BalloonTipTitle = "ASECS";
        }

        private void Menu_Opciones_Grabacion_Tiempo_Click(object sender, EventArgs e)
        {
            Menu_Tiempo_Grabacion Tiempos = new Menu_Tiempo_Grabacion(this);
            Tiempos.Show();
        }
    }
}
