using System;
using Microsoft.AspNetCore.Mvc;
using nowa_aplikacja.Models;
using nowa_aplikacja.Repositories;

namespace nowa_aplikacja.Controllers
{
    // Kontroler obsługujący akcje związane z wartościami
    public class ValuesController : Controller
    {
        // Pole do przechowywania repozytorium zadań
        private readonly ITaskRepository _taskRepository;

        // Konstruktor przyjmujący repozytorium zadań jako zależność
        public ValuesController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // Akcja wyświetlająca listę aktywnych zadań
        public IActionResult Index(string location)
        {
            try
            {
                // Ustawienie lokalizacji do wyświetlenia w widoku, domyślnie Warszawa
                ViewBag.Location = location ?? "Warsaw";
                // Pobranie wszystkich aktywnych zadań
                var tasks = _taskRepository.GetAllActive();
                // Zwrócenie widoku z listą zadań
                return View(tasks);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Index: {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja wyświetlająca szczegóły konkretnego zadania
        public ActionResult Details(int id)
        {
            try
            {
                // Pobranie zadania na podstawie id
                var task = _taskRepository.Get(id);
                // Zwrócenie widoku ze szczegółami zadania
                return View(task);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Details: {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja wyświetlająca formularz tworzenia nowego zadania (GET)
        public ActionResult Create()
        {
            try
            {
                // Zwrócenie widoku z pustym modelem zadania
                return View(new TaskModel());
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Create (GET): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja obsługująca przesłanie formularza tworzenia nowego zadania (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskModel taskModel)
        {
            try
            {
                // Sprawdzenie, czy model jest poprawny
                if (!ModelState.IsValid)
                {
                    // Zwrócenie widoku z błędami walidacji
                    return View(taskModel);
                }

                // Dodanie nowego zadania do repozytorium
                _taskRepository.Add(taskModel);
                // Przekierowanie do akcji Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Create (POST): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja wyświetlająca formularz edycji zadania (GET)
        public ActionResult Edit(int id)
        {
            try
            {
                // Pobranie zadania na podstawie id
                var task = _taskRepository.Get(id);
                // Zwrócenie widoku z modelem zadania
                return View(task);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Edit (GET): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja obsługująca przesłanie formularza edycji zadania (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TaskModel taskModel)
        {
            try
            {
                // Sprawdzenie, czy model jest poprawny
                if (!ModelState.IsValid)
                {
                    // Zwrócenie widoku z błędami walidacji
                    return View(taskModel);
                }

                // Aktualizacja zadania w repozytorium
                _taskRepository.Update(id, taskModel);
                // Przekierowanie do akcji Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Edit (POST): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja wyświetlająca potwierdzenie usunięcia zadania (GET)
        public ActionResult Delete(int id)
        {
            try
            {
                // Pobranie zadania na podstawie id
                var task = _taskRepository.Get(id);
                // Zwrócenie widoku z modelem zadania
                return View(task);
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Delete (GET): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja obsługująca usunięcie zadania (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TaskModel taskModel)
        {
            try
            {
                // Usunięcie zadania z repozytorium
                _taskRepository.Delete(id);
                // Przekierowanie do akcji Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Delete (POST): {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }

        // Akcja oznaczająca zadanie jako wykonane
        public ActionResult Done(int id)
        {
            try
            {
                // Pobranie zadania na podstawie id
                var task = _taskRepository.Get(id);
                // Ustawienie zadania jako wykonane
                task.Done = true;
                // Aktualizacja zadania w repozytorium
                _taskRepository.Update(id, task);
                // Przekierowanie do akcji Index
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Logowanie błędu
                Console.WriteLine($"An error occurred in Done: {ex.Message}");
                // Zwrócenie widoku błędu
                return View("Error");
            }
        }
    }
}
