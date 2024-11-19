using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Professions
{
    private static List<string> _peacefulProfessions = new List<string>() {"����������", "������" , "��������" , "��������", "������������", "�������", "��������� ����", "�����������",
        "��������", "������", "�����", "���������", "��������", "��������", "����������", "�������", "�������������", "���������", "�����", "���������", "�������",
        "�������", "��������", "������� �� �������", "�������", "����������", "����" };

    private static List<string> _gooseProfessions = new List<string>() {"����������", "������" , "��������" , "��������", "������������", "�������", "��������� ����", "�����������",
        "��������", "������", "�����", "���������", "��������", "��������", "����������", "�������", "�������������", "���������", "�����", "���������", "�������",
        "�������", "��������", "������� �� �������", "�������", "����������", "������", "������" };

    private static List<string> _duckProfessions = new List<string>() {"������", "��������", "����", "����������", "������������",
        "�����", "�������", "���������", "���������", "������", "���������", "���������", "�������� ������", "������", "�����", "�����������", "������", "�������",
    "����", "��� ���������", "�������"};

    public static List<string> PeacefulProfessions => _peacefulProfessions;

    public static List<string> GooseProfessions => _gooseProfessions;
    public static List<string> DuckProfessions => _duckProfessions; 
}
