using Characters;
using Controllers;
using Factories;
using Services;
using System.Collections;
using UnityEngine;
using Utils;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private RigidbodyCharacter _character;

    private ControllersFactory _controllersFactory;

    private ControllersUpdateService _controllersUpdateService;

    private void Awake() => StartCoroutine(BootstrapProcess());

    private IEnumerator BootstrapProcess()
    {
        CreateServices();

        LoadResources();

        InitializeServices();

        StartGame();

        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }

    private void CreateServices()
    {
        _controllersFactory = new ControllersFactory();
        _controllersUpdateService = new ControllersUpdateService();
    }

    private void LoadResources()
    {
    }

    private void InitializeServices()
    {
        _character.Initialize();

        Rigidbody2D rigidbody = _character.GetComponent<Rigidbody2D>();

        Controller controller = _controllersFactory.CreateMainHeroController(_character, 80);
        controller.Enable();

        _controllersUpdateService.Add(controller, () => false);
    }

    private void StartGame()
    {
    }
}
