using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaZoologico
{
    internal class Program
    {
        // =========================================================================
        // "TABLAS" SIMULADAS USANDO LISTAS BÁSICAS (SIN USAR CLASES PERSONALIZADAS)
        // =========================================================================

        // 1. EMPLEADOS
        static List<string> empDui = new List<string>();
        static List<string> empNombre = new List<string>();
        static List<string> empTelefono = new List<string>();
        static List<string> empCargo = new List<string>();
        static List<double> empSalarioBase = new List<double>();

        // 1.2 HORAS EXTRAS
        static List<string> horaExtraDui = new List<string>();
        static List<int> horaExtraCant = new List<int>();
        static List<string> horaExtraMotivo = new List<string>();
        static List<double> horaExtraTarifa = new List<double>();

        // 2. CLIENTES Y BOLETOS
        static List<string> clienteNombre = new List<string>();
        static List<string> boletoTipo = new List<string>();
        static List<double> boletoPrecio = new List<double>();
        static List<string> boletoFecha = new List<string>();
        static List<string> boletoDuiEmpleado = new List<string>();

        // 3. HÁBITATS
        static List<int> habitatId = new List<int>();
        static List<string> habitatNombre = new List<string>();
        static List<string> habitatTipo = new List<string>();
        static int contadorHabitatId = 1;

        // 3.2 ANIMALES
        static List<int> animalId = new List<int>();
        static List<string> animalNombre = new List<string>();
        static List<string> animalEspecie = new List<string>();
        static List<string> animalGenero = new List<string>();
        static List<int> animalEdad = new List<int>();
        static List<string> animalOrigen = new List<string>();
        static List<int> animalIdHabitat = new List<int>();
        static int contadorAnimalId = 1;

        // 4. DIETAS
        static List<int> dietaIdAnimal = new List<int>();
        static List<string> dietaTipo = new List<string>();
        static List<string> dietaHorario = new List<string>();

        // 4.2 SALUD
        static List<int> saludIdAnimal = new List<int>();
        static List<string> saludEstado = new List<string>();
        static List<double> saludPeso = new List<double>();
        static List<string> saludFechaRevision = new List<string>();

        // 4.3 ASIGNACIÓN DE CUIDADO
        static List<string> asignacionDui = new List<string>();
        static List<int> asignacionIdAnimal = new List<int>();
        static List<string> asignacionTarea = new List<string>();
        static List<string> asignacionFecha = new List<string>();

        // =========================================================================
        // MÉTODO PRINCIPAL (MENÚ BÁSICO)
        // =========================================================================
        static void Main(string[] args)
        {
            int opcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("==================================================");
                Console.WriteLine("    SISTEMA DE GESTIÓN DEL ZOOLÓGICO");
                Console.WriteLine("==================================================");
                Console.WriteLine("1. Módulo de Personal y Pagos");
                Console.WriteLine("2. Módulo de Comprar Boleto de Entrada");
                Console.WriteLine("3. Módulo de Animales y Hábitats");
                Console.WriteLine("4. Módulo de Salud, Alimentación y Asignación");
                Console.WriteLine("5. Salir del Sistema");
                Console.WriteLine("==================================================");

                opcion = LeerEnteroPositivo("Seleccione una opción (1-5): ");

                switch (opcion)
                {
                    case 1: MenuPersonalYPagos(); break;
                    case 2: MenuBoletos(); break;
                    case 3: MenuAnimalesYHabitats(); break;
                    case 4: MenuSaludYAlimentacion(); break;
                    case 5: Console.WriteLine("\n¡Gracias por usar el sistema!"); break;
                    default: Console.WriteLine("\nOpción no válida. Presione Enter..."); Console.ReadLine(); break;
                }
            } while (opcion != 5);
        }

        // =========================================================================
        // SECCIÓN 1: PERSONAL Y PAGOS
        // =========================================================================
        static void MenuPersonalYPagos()
        {
            int subOpcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO 1: PERSONAL Y PAGOS ---");
                Console.WriteLine("1. Agregar Empleado");
                Console.WriteLine("2. Editar Empleado");
                Console.WriteLine("3. Eliminar Empleado");
                Console.WriteLine("4. Consultar Empleados");
                Console.WriteLine("5. Registrar Horas Extras");
                Console.WriteLine("6. Consultar Horas Extras");
                Console.WriteLine("7. Calcular y Generar Pago (Nómina)");
                Console.WriteLine("8. Volver al Menú Principal");

                subOpcion = LeerEnteroPositivo("Seleccione una opción: ");

                switch (subOpcion)
                {
                    case 1: AgregarEmpleado(); break;
                    case 2: EditarEmpleado(); break;
                    case 3: EliminarEmpleado(); break;
                    case 4: ConsultarEmpleados(); break;
                    case 5: RegistrarHorasExtras(); break;
                    case 6: ConsultarHorasExtras(); break;
                    case 7: CalcularNomina(); break;
                }
                if (subOpcion != 8) { Console.WriteLine("\nPresione Enter para continuar..."); Console.ReadLine(); }
            } while (subOpcion != 8);
        }

        static void AgregarEmpleado()
        {
            Console.WriteLine("\n--- AGREGAR EMPLEADO ---");
            string dui = LeerDuiFormateado("Ingrese el DUI (ej. 01234567-8): ");

            // Validar que el DUI no exista previamente
            if (BuscarPosicionEmpleado(dui) != -1)
            {
                Console.WriteLine("Error: Ya existe un empleado registrado con ese DUI.");
                return;
            }

            string nombre = LeerTextoSoloLetras("Ingrese Nombre Completo: ");
            string telefono = LeerTextoNumerico("Ingrese Teléfono: ");

            Console.WriteLine("\nCargos disponibles: 1.Veterinario | 2.Cuidador | 3.Mantenimiento | 4.Taquillero | 5.Seguridad | 6.Guiador");
            string cargo = LeerOpcionCargo();

            double salarioBase = LeerDoublePositivo("Ingrese Salario Base ($): ");

            // Guardar en las listas
            empDui.Add(dui);
            empNombre.Add(nombre);
            empTelefono.Add(telefono);
            empCargo.Add(cargo);
            empSalarioBase.Add(salarioBase);

            Console.WriteLine("\n¡Empleado registrado exitosamente!");
        }

        static void EditarEmpleado()
        {
            Console.WriteLine("\n--- EDITAR EMPLEADO ---");
            string dui = LeerTextoNoVacio("Ingrese el DUI del empleado a editar: ");
            int pos = BuscarPosicionEmpleado(dui);

            if (pos == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            Console.WriteLine("Modificando los datos de: " + empNombre[pos]);
            empNombre[pos] = LeerTextoSoloLetras("Nuevo Nombre Completo: ");
            empTelefono[pos] = LeerTextoNumerico("Nuevo Teléfono: ");
            empCargo[pos] = LeerOpcionCargo();
            empSalarioBase[pos] = LeerDoublePositivo("Nuevo Salario Base ($): ");

            Console.WriteLine("\n¡Empleado modificado correctamente!");
        }

        static void EliminarEmpleado()
        {
            Console.WriteLine("\n--- ELIMINAR EMPLEADO ---");
            string dui = LeerTextoNoVacio("Ingrese el DUI del empleado a eliminar: ");
            int pos = BuscarPosicionEmpleado(dui);

            if (pos == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            empDui.RemoveAt(pos);
            empNombre.RemoveAt(pos);
            empTelefono.RemoveAt(pos);
            empCargo.RemoveAt(pos);
            empSalarioBase.RemoveAt(pos);

            Console.WriteLine("\n¡Empleado eliminado del sistema!");
        }

        static void ConsultarEmpleados()
        {
            Console.WriteLine("\n--- LISTA GENERAL DE EMPLEADOS ---");
            if (empDui.Count == 0)
            {
                Console.WriteLine("No hay empleados registrados.");
                return;
            }

            for (int i = 0; i < empDui.Count; i++)
            {
                Console.WriteLine($"DUI: {empDui[i]} | Nombre: {empNombre[i]} | Tel: {empTelefono[i]} | Cargo: {empCargo[i]} | Salario: ${empSalarioBase[i]:F2}");
            }
        }

        static void RegistrarHorasExtras()
        {
            Console.WriteLine("\n--- REGISTRAR HORAS EXTRAS ---");
            string dui = LeerTextoNoVacio("Ingrese DUI del empleado: ");
            int pos = BuscarPosicionEmpleado(dui);

            if (pos == -1)
            {
                Console.WriteLine("El DUI no pertenece a ningún empleado registrado.");
                return;
            }

            int horas = LeerEnteroPositivo("Ingrese cantidad de horas trabajadas: ");
            string motivo = LeerTextoNoVacio("Ingrese motivo (ej. atención de parto, reparación): ");
            double tarifa = LeerDoublePositivo("Ingrese tarifa por hora ($): ");

            horaExtraDui.Add(dui);
            horaExtraCant.Add(horas);
            horaExtraMotivo.Add(motivo);
            horaExtraTarifa.Add(tarifa);

            Console.WriteLine("\n¡Horas extras registradas con éxito!");
        }

        static void ConsultarHorasExtras()
        {
            Console.WriteLine("\n--- CONSULTAR HORAS EXTRAS ---");
            string dui = LeerTextoNoVacio("Ingrese DUI del empleado: ");

            bool encontro = false;
            for (int i = 0; i < horaExtraDui.Count; i++)
            {
                if (horaExtraDui[i] == dui)
                {
                    Console.WriteLine($"Horas: {horaExtraCant[i]} hrs | Motivo: {horaExtraMotivo[i]} | Tarifa/Hora: ${horaExtraTarifa[i]:F2}");
                    encontro = true;
                }
            }

            if (!encontro) Console.WriteLine("No hay horas extras registradas para este empleado.");
        }

        static void CalcularNomina()
        {
            Console.WriteLine("\n--- CÁLCULO DE NÓMINA (RECIBO DE PAGO) ---");
            string dui = LeerTextoNoVacio("Ingrese DUI del empleado: ");
            int pos = BuscarPosicionEmpleado(dui);

            if (pos == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            double basePago = empSalarioBase[pos];
            double totalHorasExtras = 0;

            for (int i = 0; i < horaExtraDui.Count; i++)
            {
                if (horaExtraDui[i] == dui)
                {
                    totalHorasExtras += (horaExtraCant[i] * horaExtraTarifa[i]);
                }
            }

            double totalPagar = basePago + totalHorasExtras;

            Console.WriteLine("========================================");
            Console.WriteLine("RECIBO DE PAGO");
            Console.WriteLine("Empleado: " + empNombre[pos]);
            Console.WriteLine("Cargo: " + empCargo[pos]);
            Console.WriteLine($"Salario Base: ${basePago:F2}");
            Console.WriteLine($"Total Horas Extras: ${totalHorasExtras:F2}");
            Console.WriteLine($"TOTAL A PAGAR: ${totalPagar:F2}");
            Console.WriteLine("========================================");
        }

        // =========================================================================
        // SECCIÓN 2: COMPRAR BOLETO DE ENTRADA
        // =========================================================================
        static void MenuBoletos()
        {
            int subOpcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO 2: TAQUILLA Y BOLETOS ---");
                Console.WriteLine("1. Registrar Cliente y Vender Boleto");
                Console.WriteLine("2. Consultar Boletos Vendidos e Ingresos Totales");
                Console.WriteLine("3. Volver al Menú Principal");

                subOpcion = LeerEnteroPositivo("Seleccione una opción: ");

                switch (subOpcion)
                {
                    case 1: VenderBoleto(); break;
                    case 2: ConsultarBoletos(); break;
                }
                if (subOpcion != 3) { Console.WriteLine("\nPresione Enter para continuar..."); Console.ReadLine(); }
            } while (subOpcion != 3);
        }

        static void VenderBoleto()
        {
            Console.WriteLine("\n--- VENDER BOLETO DE ENTRADA ---");
            string duiEmp = LeerTextoNoVacio("Ingrese DUI del Empleado que atiende en Taquilla: ");
            if (BuscarPosicionEmpleado(duiEmp) == -1)
            {
                Console.WriteLine("Empleado no registrado en el sistema.");
                return;
            }

            string nombreCli = LeerTextoSoloLetras("Nombre del Cliente: ");

            Console.WriteLine("\nTipo de Boleto: 1.Niño ($3) | 2.Adulto ($5) | 3.Tercera Edad ($2.50)");
            int tipoOp = LeerEnteroPositivo("Seleccione tipo (1-3): ");

            string tipoBoletoText = "Adulto";
            double precio = 5.00;

            if (tipoOp == 1) { tipoBoletoText = "Niño"; precio = 3.00; }
            else if (tipoOp == 3) { tipoBoletoText = "Tercera Edad"; precio = 2.50; }

            string fechaHoy = DateTime.Now.ToString("dd/MM/yyyy");

            // Guardar compra
            clienteNombre.Add(nombreCli);
            boletoTipo.Add(tipoBoletoText);
            boletoPrecio.Add(precio);
            boletoFecha.Add(fechaHoy);
            boletoDuiEmpleado.Add(duiEmp);

            Console.WriteLine("\n========================================");
            Console.WriteLine("¡BOLETO GENERADO CON ÉXITO!");
            Console.WriteLine($"Cliente: {nombreCli} | Tipo: {tipoBoletoText} | Precio: ${precio:F2}");
            Console.WriteLine($"Fecha: {fechaHoy} | Atendió DUI: {duiEmp}");
            Console.WriteLine("========================================");
        }

        static void ConsultarBoletos()
        {
            Console.WriteLine("\n--- HISTORIAL DE BOLETOS VENDIDOS ---");
            if (clienteNombre.Count == 0)
            {
                Console.WriteLine("No se han vendido boletos aún.");
                return;
            }

            double totalIngresos = 0;
            for (int i = 0; i < clienteNombre.Count; i++)
            {
                Console.WriteLine($"Cliente: {clienteNombre[i]} | Tipo: {boletoTipo[i]} | Precio: ${boletoPrecio[i]:F2} | Fecha: {boletoFecha[i]} | Taquillero DUI: {boletoDuiEmpleado[i]}");
                totalIngresos += boletoPrecio[i];
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"INGRESOS TOTALES RECAUDADOS: ${totalIngresos:F2}");
        }

        // =========================================================================
        // SECCIÓN 3: ANIMALES Y HÁBITATS
        // =========================================================================
        static void MenuAnimalesYHabitats()
        {
            int subOpcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO 3: ANIMALES Y HÁBITATS ---");
                Console.WriteLine("1. Agregar Hábitat");
                Console.WriteLine("2. Editar / Eliminar Hábitat");
                Console.WriteLine("3. Consultar Hábitats");
                Console.WriteLine("4. Agregar Animal");
                Console.WriteLine("5. Editar Animal");
                Console.WriteLine("6. Eliminar Animal");
                Console.WriteLine("7. Buscar Animales por Hábitat");
                Console.WriteLine("8. Volver al Menú Principal");

                subOpcion = LeerEnteroPositivo("Seleccione una opción: ");

                switch (subOpcion)
                {
                    case 1: AgregarHabitat(); break;
                    case 2: EditarEliminarHabitat(); break;
                    case 3: ConsultarHabitats(); break;
                    case 4: AgregarAnimal(); break;
                    case 5: EditarAnimal(); break;
                    case 6: EliminarAnimal(); break;
                    case 7: BuscarAnimalesPorHabitat(); break;
                }
                if (subOpcion != 8) { Console.WriteLine("\nPresione Enter para continuar..."); Console.ReadLine(); }
            } while (subOpcion != 8);
        }

        static void AgregarHabitat()
        {
            Console.WriteLine("\n--- AGREGAR HÁBITAT ---");
            string nombre = LeerTextoNoVacio("Nombre del Recinto (ej. Aviario, Sabana): ");

            Console.WriteLine("Tipo de recinto: 1.Terrestre | 2.Acuático | 3.Aéreo");
            int tipoOp = LeerEnteroPositivo("Opción: ");
            string tipoText = (tipoOp == 2) ? "Acuático" : (tipoOp == 3) ? "Aéreo" : "Terrestre";

            habitatId.Add(contadorHabitatId);
            habitatNombre.Add(nombre);
            habitatTipo.Add(tipoText);

            Console.WriteLine($"\n¡Hábitat registrado con ID: {contadorHabitatId}!");
            contadorHabitatId++;
        }

        static void EditarEliminarHabitat()
        {
            Console.WriteLine("\n--- EDITAR / ELIMINAR HÁBITAT ---");
            int id = LeerEnteroPositivo("Ingrese el ID del Hábitat: ");
            int pos = BuscarPosicionHabitat(id);

            if (pos == -1)
            {
                Console.WriteLine("Hábitat no encontrado.");
                return;
            }

            Console.WriteLine("1. Editar Nombre y Tipo | 2. Eliminar Hábitat");
            int accion = LeerEnteroPositivo("Elija acción: ");

            if (accion == 1)
            {
                habitatNombre[pos] = LeerTextoNoVacio("Nuevo Nombre: ");
                Console.WriteLine("Tipo: 1.Terrestre | 2.Acuático | 3.Aéreo");
                int tipoOp = LeerEnteroPositivo("Opción: ");
                habitatTipo[pos] = (tipoOp == 2) ? "Acuático" : (tipoOp == 3) ? "Aéreo" : "Terrestre";
                Console.WriteLine("¡Hábitat actualizado!");
            }
            else if (accion == 2)
            {
                // Verificar si hay animales habitando en este recinto
                bool tieneAnimales = false;
                for (int i = 0; i < animalIdHabitat.Count; i++)
                {
                    if (animalIdHabitat[i] == id) { tieneAnimales = true; break; }
                }

                if (tieneAnimales)
                {
                    Console.WriteLine("No se puede eliminar el hábitat porque tiene animales asignados.");
                }
                else
                {
                    habitatId.RemoveAt(pos);
                    habitatNombre.RemoveAt(pos);
                    habitatTipo.RemoveAt(pos);
                    Console.WriteLine("¡Hábitat eliminado exitosamente!");
                }
            }
        }

        static void ConsultarHabitats()
        {
            Console.WriteLine("\n--- LISTA DE HÁBITATS DISPONIBLES ---");
            if (habitatId.Count == 0)
            {
                Console.WriteLine("No hay hábitats registrados.");
                return;
            }

            for (int i = 0; i < habitatId.Count; i++)
            {
                Console.WriteLine($"ID: {habitatId[i]} | Recinto: {habitatNombre[i]} | Tipo: {habitatTipo[i]}");
            }
        }

        static void AgregarAnimal()
        {
            Console.WriteLine("\n--- AGREGAR ANIMAL ---");
            if (habitatId.Count == 0)
            {
                Console.WriteLine("Debe registrar al menos un hábitat antes de agregar animales.");
                return;
            }

            string nombre = LeerTextoSoloLetras("Nombre del Animal: ");
            string especie = LeerTextoSoloLetras("Especie (ej. León, Águila): ");

            Console.WriteLine("Género: 1.Macho | 2.Hembra");
            string genero = (LeerEnteroPositivo("Opción: ") == 2) ? "Hembra" : "Macho";

            int edad = LeerEnteroPositivo("Edad (años): ");

            Console.WriteLine("Origen: 1.Nacido en Cautiverio | 2.Rescatado | 3.Donado/Intercambiado");
            int origOp = LeerEnteroPositivo("Opción: ");
            string origen = (origOp == 2) ? "Rescatado" : (origOp == 3) ? "Donado/Intercambiado" : "Nacido en Cautiverio";

            ConsultarHabitats();
            int idHab = LeerEnteroPositivo("Ingrese ID del Hábitat asignado: ");
            if (BuscarPosicionHabitat(idHab) == -1)
            {
                Console.WriteLine("ID de Hábitat no válido.");
                return;
            }

            animalId.Add(contadorAnimalId);
            animalNombre.Add(nombre);
            animalEspecie.Add(especie);
            animalGenero.Add(genero);
            animalEdad.Add(edad);
            animalOrigen.Add(origen);
            animalIdHabitat.Add(idHab);

            Console.WriteLine($"\n¡Animal registrado exitosamente con ID: {contadorAnimalId}!");
            contadorAnimalId++;
        }

        static void EditarAnimal()
        {
            Console.WriteLine("\n--- EDITAR ANIMAL ---");
            int id = LeerEnteroPositivo("Ingrese el ID del Animal a editar: ");
            int pos = BuscarPosicionAnimal(id);

            if (pos == -1)
            {
                Console.WriteLine("Animal no encontrado.");
                return;
            }

            Console.WriteLine("Modificando los datos de: " + animalNombre[pos]);
            animalEdad[pos] = LeerEnteroPositivo("Nueva Edad (años): ");

            ConsultarHabitats();
            int nuevoHab = LeerEnteroPositivo("Nuevo ID de Hábitat: ");
            if (BuscarPosicionHabitat(nuevoHab) != -1)
            {
                animalIdHabitat[pos] = nuevoHab;
            }

            Console.WriteLine("¡Datos del animal actualizados!");
        }

        static void EliminarAnimal()
        {
            Console.WriteLine("\n--- ELIMINAR ANIMAL ---");
            int id = LeerEnteroPositivo("Ingrese ID del animal a dar de baja: ");
            int pos = BuscarPosicionAnimal(id);

            if (pos == -1)
            {
                Console.WriteLine("Animal no encontrado.");
                return;
            }

            animalId.RemoveAt(pos);
            animalNombre.RemoveAt(pos);
            animalEspecie.RemoveAt(pos);
            animalGenero.RemoveAt(pos);
            animalEdad.RemoveAt(pos);
            animalOrigen.RemoveAt(pos);
            animalIdHabitat.RemoveAt(pos);

            Console.WriteLine("¡Animal dado de baja del sistema!");
        }

        static void BuscarAnimalesPorHabitat()
        {
            Console.WriteLine("\n--- BUSCAR ANIMALES POR HÁBITAT ---");
            ConsultarHabitats();
            int idHab = LeerEnteroPositivo("Ingrese el ID del Hábitat a consultar: ");

            bool encontro = false;
            for (int i = 0; i < animalId.Count; i++)
            {
                if (animalIdHabitat[i] == idHab)
                {
                    Console.WriteLine($"ID Animal: {animalId[i]} | Nombre: {animalNombre[i]} | Especie: {animalEspecie[i]} | Edad: {animalEdad[i]} años");
                    encontro = true;
                }
            }

            if (!encontro) Console.WriteLine("No hay animales registrados en este recinto.");
        }

        // =========================================================================
        // SECCIÓN 4: SALUD, ALIMENTACIÓN Y ASIGNACIÓN DE CUIDADO
        // =========================================================================
        static void MenuSaludYAlimentacion()
        {
            int subOpcion = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("--- MÓDULO 4: SALUD, ALIMENTACIÓN Y ASIGNACIÓN ---");
                Console.WriteLine("1. Asignar / Editar Dieta a un Animal");
                Console.WriteLine("2. Consultar Dieta de un Animal");
                Console.WriteLine("3. Registrar / Actualizar Ficha Médica de Salud");
                Console.WriteLine("4. Consultar Historial Médico");
                Console.WriteLine("5. Asignar Tarea de Cuidado (Empleado <-> Animal)");
                Console.WriteLine("6. Consultar Tareas Asignadas");
                Console.WriteLine("7. Volver al Menú Principal");

                subOpcion = LeerEnteroPositivo("Seleccione una opción: ");

                switch (subOpcion)
                {
                    case 1: AsignarDieta(); break;
                    case 2: ConsultarDieta(); break;
                    case 3: RegistrarSalud(); break;
                    case 4: ConsultarSalud(); break;
                    case 5: AsignarTareaCuidado(); break;
                    case 6: ConsultarTareas(); break;
                }
                if (subOpcion != 7) { Console.WriteLine("\nPresione Enter para continuar..."); Console.ReadLine(); }
            } while (subOpcion != 7);
        }

        static void AsignarDieta()
        {
            Console.WriteLine("\n--- ASIGNAR DIETA Y ALIMENTACIÓN ---");
            int idAnim = LeerEnteroPositivo("Ingrese el ID del Animal: ");
            if (BuscarPosicionAnimal(idAnim) == -1)
            {
                Console.WriteLine("ID de animal no válido.");
                return;
            }

            Console.WriteLine("Tipo de Dieta: 1.Carnívoro | 2.Herbívoro | 3.Omnívoro");
            int tipoOp = LeerEnteroPositivo("Opción: ");
            string tipoDieta = (tipoOp == 1) ? "Carnívoro" : (tipoOp == 2) ? "Herbívoro" : "Omnívoro";

            string horario = LeerTextoNoVacio("Horario de comida (ej. 08:00 AM y 04:00 PM): ");

            // Buscar si ya tiene dieta para actualizar o agregar nueva
            int posDieta = -1;
            for (int i = 0; i < dietaIdAnimal.Count; i++)
            {
                if (dietaIdAnimal[i] == idAnim) { posDieta = i; break; }
            }

            if (posDieta != -1)
            {
                dietaTipo[posDieta] = tipoDieta;
                dietaHorario[posDieta] = horario;
                Console.WriteLine("¡Dieta actualizada correctamente!");
            }
            else
            {
                dietaIdAnimal.Add(idAnim);
                dietaTipo.Add(tipoDieta);
                dietaHorario.Add(horario);
                Console.WriteLine("¡Dieta asignada con éxito!");
            }
        }

        static void ConsultarDieta()
        {
            Console.WriteLine("\n--- CONSULTAR PLAN DE ALIMENTACIÓN ---");
            int idAnim = LeerEnteroPositivo("Ingrese ID del Animal: ");

            bool encontro = false;
            for (int i = 0; i < dietaIdAnimal.Count; i++)
            {
                if (dietaIdAnimal[i] == idAnim)
                {
                    Console.WriteLine($"ID Animal: {idAnim} | Tipo Dieta: {dietaTipo[i]} | Horario: {dietaHorario[i]}");
                    encontro = true;
                }
            }

            if (!encontro) Console.WriteLine("El animal no tiene dieta asignada.");
        }

        static void RegistrarSalud()
        {
            Console.WriteLine("\n--- REGISTRAR / ACTUALIZAR FICHA MÉDICA ---");
            int idAnim = LeerEnteroPositivo("Ingrese ID del Animal: ");
            if (BuscarPosicionAnimal(idAnim) == -1)
            {
                Console.WriteLine("Animal no registrado.");
                return;
            }

            double peso = LeerDoublePositivo("Ingrese Peso en Libras (Lbs): ");

            Console.WriteLine("Estado de Salud: 1.Bueno | 2.En Tratamiento");
            string estado = (LeerEnteroPositivo("Opción: ") == 2) ? "En Tratamiento" : "Bueno";

            string fechaHoy = DateTime.Now.ToString("dd/MM/yyyy");

            saludIdAnimal.Add(idAnim);
            saludEstado.Add(estado);
            saludPeso.Add(peso);
            saludFechaRevision.Add(fechaHoy);

            Console.WriteLine("¡Ficha médica guardada correctamente!");
        }

        static void ConsultarSalud()
        {
            Console.WriteLine("\n--- HISTORIAL MÉDICO DEL ANIMAL ---");
            int idAnim = LeerEnteroPositivo("Ingrese ID del Animal: ");

            bool encontro = false;
            for (int i = 0; i < saludIdAnimal.Count; i++)
            {
                if (saludIdAnimal[i] == idAnim)
                {
                    Console.WriteLine($"Fecha Revision: {saludFechaRevision[i]} | Peso: {saludPeso[i]} Lbs | Estado: {saludEstado[i]}");
                    encontro = true;
                }
            }

            if (!encontro) Console.WriteLine("No existe historial médico para este animal.");
        }

        static void AsignarTareaCuidado()
        {
            Console.WriteLine("\n--- ASIGNAR TAREA DE CUIDADO ---");
            string dui = LeerTextoNoVacio("Ingrese DUI del Empleado: ");
            if (BuscarPosicionEmpleado(dui) == -1)
            {
                Console.WriteLine("Empleado no encontrado.");
                return;
            }

            int idAnim = LeerEnteroPositivo("Ingrese ID del Animal: ");
            if (BuscarPosicionAnimal(idAnim) == -1)
            {
                Console.WriteLine("Animal no encontrado.");
                return;
            }

            Console.WriteLine("Tareas: 1.Alimentación | 2.Limpieza | 3.Chequeo | 4.Reparaciones | 5.Guiado");
            int tOp = LeerEnteroPositivo("Seleccione tarea: ");
            string tareaText = "Alimentación";
            if (tOp == 2) tareaText = "Limpieza";
            else if (tOp == 3) tareaText = "Chequeo";
            else if (tOp == 4) tareaText = "Reparaciones";
            else if (tOp == 5) tareaText = "Guiado";

            string fecha = DateTime.Now.ToString("dd/MM/yyyy");

            asignacionDui.Add(dui);
            asignacionIdAnimal.Add(idAnim);
            asignacionTarea.Add(tareaText);
            asignacionFecha.Add(fecha);

            Console.WriteLine("\n¡Tarea de cuidado asignada exitosamente!");
        }

        static void ConsultarTareas()
        {
            Console.WriteLine("\n--- CONSULTAR TAREAS DE CUIDADO ---");
            Console.WriteLine("1. Buscar por Empleado (DUI) | 2. Buscar por Animal (ID)");
            int op = LeerEnteroPositivo("Opción: ");

            if (op == 1)
            {
                string dui = LeerTextoNoVacio("Ingrese DUI: ");
                for (int i = 0; i < asignacionDui.Count; i++)
                {
                    if (asignacionDui[i] == dui)
                    {
                        Console.WriteLine($"Atiende al Animal ID: {asignacionIdAnimal[i]} | Tarea: {asignacionTarea[i]} | Fecha: {asignacionFecha[i]}");
                    }
                }
            }
            else
            {
                int id = LeerEnteroPositivo("Ingrese ID Animal: ");
                for (int i = 0; i < asignacionIdAnimal.Count; i++)
                {
                    if (asignacionIdAnimal[i] == id)
                    {
                        Console.WriteLine($"Atendido por Empleado DUI: {asignacionDui[i]} | Tarea: {asignacionTarea[i]} | Fecha: {asignacionFecha[i]}");
                    }
                }
            }
        }

        // =========================================================================
        // MÉTODOS DE BÚSQUEDA AUXILIARES EN LISTAS
        // =========================================================================
        static int BuscarPosicionEmpleado(string dui)
        {
            for (int i = 0; i < empDui.Count; i++)
            {
                if (empDui[i] == dui) return i;
            }
            return -1;
        }

        static int BuscarPosicionHabitat(int id)
        {
            for (int i = 0; i < habitatId.Count; i++)
            {
                if (habitatId[i] == id) return i;
            }
            return -1;
        }

        static int BuscarPosicionAnimal(int id)
        {
            for (int i = 0; i < animalId.Count; i++)
            {
                if (animalId[i] == id) return i;
            }
            return -1;
        }

        // =========================================================================
        // FUNCIONES DE VALIDACIÓN DE ENTRADAS (ESTRICTAS Y REUTILIZABLES)
        // =========================================================================

        // Valida que el texto no contenga números ni símbolos (Solo letras y espacios)
        static string LeerTextoSoloLetras(string mensaje)
        {
            string entrada;
            bool esValido;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine().Trim();
                esValido = true;

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    esValido = false;
                }
                else
                {
                    foreach (char c in entrada)
                    {
                        if (!char.IsLetter(c) && c != ' ')
                        {
                            esValido = false;
                            break;
                        }
                    }
                }

                if (!esValido)
                {
                    Console.WriteLine(">> ERROR: Ingrese solo texto (sin números ni símbolos especiales).");
                }
            } while (!esValido);

            return entrada;
        }

        // Valida que solo sean números (ej. para teléfonos sin guiones)
        static string LeerTextoNumerico(string mensaje)
        {
            string entrada;
            bool esValido;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine().Trim();
                esValido = true;

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    esValido = false;
                }
                else
                {
                    foreach (char c in entrada)
                    {
                        if (!char.IsDigit(c))
                        {
                            esValido = false;
                            break;
                        }
                    }
                }

                if (!esValido)
                {
                    Console.WriteLine(">> ERROR: Ingrese únicamente números (sin letras ni espacios).");
                }
            } while (!esValido);

            return entrada;
        }

        // Valida que el texto no esté vacío (para cualquier cadena general)
        static string LeerTextoNoVacio(string mensaje)
        {
            string entrada;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.WriteLine(">> ERROR: El campo no puede quedar vacío.");
                }
            } while (string.IsNullOrWhiteSpace(entrada));

            return entrada;
        }

        // Valida formato DUI (ej. 01234567-8)
        static string LeerDuiFormateado(string mensaje)
        {
            string entrada;
            do
            {
                Console.Write(mensaje);
                entrada = Console.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(entrada) || entrada.Length < 9)
                {
                    Console.WriteLine(">> ERROR: Formato de DUI no válido.");
                }
                else
                {
                    break;
                }
            } while (true);

            return entrada;
        }

        // Valida selección del cargo permitido en el documento
        static string LeerOpcionCargo()
        {
            int op;
            do
            {
                op = LeerEnteroPositivo("Seleccione Cargo (1-6): ");
                switch (op)
                {
                    case 1: return "Veterinario";
                    case 2: return "Cuidador";
                    case 3: return "Mantenimiento";
                    case 4: return "Taquillero";
                    case 5: return "Seguridad";
                    case 6: return "Guiador";
                    default:
                        Console.WriteLine(">> ERROR: Elija una opción entre 1 y 6.");
                        break;
                }
            } while (true);
        }

        // Valida el ingreso de números enteros mayores que cero (reemplaza letras por error)
        static int LeerEnteroPositivo(string mensaje)
        {
            int numero;
            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (int.TryParse(entrada, out numero) && numero > 0)
                {
                    return numero;
                }
                Console.WriteLine(">> ERROR: Debe ingresar un número entero mayor a 0.");
            } while (true);
        }

        // Valida el ingreso de números decimales/dinero (reemplaza letras por error)
        static double LeerDoublePositivo(string mensaje)
        {
            double numero;
            do
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();
                if (double.TryParse(entrada, out numero) && numero >= 0)
                {
                    return numero;
                }
                Console.WriteLine(">> ERROR: Debe ingresar un valor numérico positivo.");
            } while (true);
        }
    }
}
