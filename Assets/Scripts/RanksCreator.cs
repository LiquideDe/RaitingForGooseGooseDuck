using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using TMPro;
using UnityEngine.UI;

public class RanksCreator : MonoBehaviour
{
    [SerializeField] private List<PlayerOptions> _playerOptions;
    [SerializeField] private DatabaseController _databaseController;
    [SerializeField] private PlayerView _playerPrefab;
    [SerializeField] private PlayerView _gooseFirst, _gooseSecond, _gooseThird;
    [SerializeField] private Transform _content;
    [SerializeField] private TextMeshProUGUI _nameScene;
    [SerializeField] private Sprite _peaceBackground, _duckBackground, _pigeonBackground, _pelicanBackground, _dodoBackground;
    [SerializeField] private Image _background;
    private List<Player> _players = new List<Player>();
    private List<GameObject> _gameObjects = new List<GameObject>();
    private enum TypeProfessions { Goose, Duck, Pigeon, Pelican, Dodo, AllDuck, AllGoose, BestDuck, BestGoose, AllBest, BestDodo, BestPelican, BestPigeon, Kicked}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(BuildRank());
        }
    }

    IEnumerator BuildRank()
    {
        foreach(PlayerOptions player in _playerOptions)
        {
            _players.Add(new Player(player));
            _players[^1].SetTotalGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {_players[^1].Name}"));
        }

        List<string> gooseProfessions = new List<string>() { "����������", "������" , "��������" , "��������", "������������", "�������", "��������� ����", "�����������",
        "��������", "������", "�����", "���������", "��������", "��������", "����������", "�������", "�������������", "���������", "�����", "���������", "�������",
        "�������"};

        List<string> duckProfessions = new List<string>() { "�������", "���������" , "��������", "����", "���������", "���������", "�����������", "������������",
        "�������� ������", "����������", "������", "�����", "�����", "���������", "������", "������", "��������", "������", "�������"};
        
        foreach(string prof in gooseProfessions)
        {
            _nameScene.text = $"����� ������ {prof}";
            yield return TakeDataAndSortPlayers(prof, TypeProfessions.Goose);
        }
        _nameScene.text = $"���� ����� ���� ������";
        yield return TakeDataAndSortPlayers("", TypeProfessions.AllGoose);

        _background.sprite = _duckBackground;

        foreach(string duckProd in duckProfessions)
        {
            _nameScene.text = $"����� ������ {duckProd}";
            yield return TakeDataAndSortPlayers(duckProd, TypeProfessions.Duck);
        }
        _nameScene.text = $"���� ����� ���� ������";
        yield return TakeDataAndSortPlayers("", TypeProfessions.AllDuck);
        
        _background.sprite = _pigeonBackground;
        _nameScene.text = $"���� ����� ���� �������";
        yield return TakeDataAndSortPlayers("������", TypeProfessions.Pigeon);

        _nameScene.text = $"������ ������";
        yield return TakeDataAndSortPlayers("", TypeProfessions.BestPigeon);

        _background.sprite = _pelicanBackground;
        _nameScene.text = $"���� ����� ���� ���������";
        yield return TakeDataAndSortPlayers("�������", TypeProfessions.Pelican);

        _nameScene.text = $"������ �������";
        yield return TakeDataAndSortPlayers("", TypeProfessions.BestPelican);

        _background.sprite = _dodoBackground;
        _nameScene.text = $"���� ����� ���� ����";
        yield return TakeDataAndSortPlayers("����", TypeProfessions.Dodo);

        _nameScene.text = $"������ ����";
        yield return TakeDataAndSortPlayers("", TypeProfessions.BestDodo);

        _background.sprite = _peaceBackground;
        _nameScene.text = $"���� ����� �������� �� ����";
        yield return TakeDataAndSortPlayers("", TypeProfessions.Kicked);

        _nameScene.text = $"������ ����";
        yield return TakeDataAndSortPlayers("", TypeProfessions.BestGoose);

        _background.sprite = _duckBackground;
        _nameScene.text = $"������ ����";
        yield return TakeDataAndSortPlayers("", TypeProfessions.BestDuck);

        _background.sprite = _peaceBackground;
        _nameScene.text = $"������ �����";
        yield return TakeDataAndSortPlayers("", TypeProfessions.AllBest);
    }

    IEnumerator TakeDataAndSortPlayers(string profession, TypeProfessions typeProfessions)
    {
        foreach (Player player in _players)
        {
            if (typeProfessions == TypeProfessions.Goose)
                player.AddGooseGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}'"),
                    _databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}' AND IsWining = 1"));

            else if (typeProfessions == TypeProfessions.Duck)
                player.AddDuckGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}'"),
                _databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}' AND IsWining = 1"));

            else if (typeProfessions == TypeProfessions.Pigeon)
                player.AddPigeonGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}'"),
                _databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}' AND IsWining = 1"));

            else if (typeProfessions == TypeProfessions.Pelican)
                player.AddPelicanGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}'"),
                _databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}' AND IsWining = 1"));

            else if (typeProfessions == TypeProfessions.Dodo)
                player.AddDodoGames(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}'"),
                _databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE Profession = '{profession}' AND IsWining = 1"));

            else if (typeProfessions == TypeProfessions.AllDuck)
                player.CalculateDuckGames();

            else if (typeProfessions == TypeProfessions.AllGoose)
                player.CalculateGooseGames();

            else if (typeProfessions == TypeProfessions.BestPigeon)
                player.CalculatePigeonRate();

            else if (typeProfessions == TypeProfessions.BestPelican)
                player.CalculatePelicanRate();

            else if (typeProfessions == TypeProfessions.BestDodo)
                player.CalculateDodoRate();

            else if (typeProfessions == TypeProfessions.BestGoose)
                player.CalculateGooseRate();

            else if (typeProfessions == TypeProfessions.BestDuck)
                player.CalculateDuckRate();

            else if (typeProfessions == TypeProfessions.AllBest)
                player.CalculateTotalWinRate();

            else if (typeProfessions == TypeProfessions.Kicked)
                player.CalculateKickedRate(_databaseController.ExecuteOrder($"SELECT Count(IsWining) FROM {player.Name} WHERE FinalState = '�������'")); ;

        }

        _players.Sort(
            delegate (Player cb1, Player cb2)
            {
                return cb2.Rate.CompareTo(cb1.Rate);
            }
            );

        yield return CreatePlayers();
    }

    IEnumerator CreatePlayers()
    {
        if (_gameObjects.Count > 0)
            yield return CleanScene();

        for (int i = 3; i < _players.Count; i++)
        {
            PlayerView playerView = Instantiate(_playerPrefab, _content);
            playerView.Initialize($"{i + 1} - {_players[i].Name} {_players[i].Rate}%", _players[i].PlayerOptions.HalfSize);
            playerView.gameObject.SetActive(true);
            _gameObjects.Add(playerView.gameObject);
            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1f);

        _gooseThird.Initialize($"{3} - {_players[2].Name} {_players[2].Rate}%", _players[2].PlayerOptions.FullSize);
        _gooseThird.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _gooseSecond.Initialize($"{2} - {_players[1].Name} {_players[1].Rate}%", _players[1].PlayerOptions.FullSize);
        _gooseSecond.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        _gooseFirst.Initialize($"{1} - {_players[0].Name} {_players[0].Rate}%", _players[0].PlayerOptions.FullSize);
        _gooseFirst.gameObject.SetActive(true);

        yield return new WaitForSeconds(3f);
    }

    IEnumerator CleanScene()
    {
        foreach(GameObject gameObject in _gameObjects)
        {
            Destroy(gameObject);
        }

        _gameObjects.Clear();
        _gooseFirst.gameObject.SetActive(false);
        _gooseSecond.gameObject.SetActive(false);
        _gooseThird.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.3f);
    }
}
