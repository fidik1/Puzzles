using UnityEngine;

public struct CalculateSides
{
    private readonly Color32[] _color;

    public CalculateSides(Color32[] color) => _color = color;

    public SidesState Calculate(SidesState sidesState, SidesState leftState, int x, int y, int maxX, int maxY)
    {
        SidesState sides = new();
        for (int i = 0; i < _color.Length; i++)
            sides.color[i] = _color[i];

        for (int i = 0; i < 4; i++)
        {
            sides.edgesOuter[i] = GetRandomBool();
            sides.edgesInner[i] = sides.edgesOuter[i];
            sides.color[i] = GetRandomColor(sides);
        }

        InvertSides(1, sides, sidesState);
        if (x > 0)
            InvertSides(0, sides, leftState);
        CheckEdges(sides, x, y, maxX, maxY);
        return sides;
    }

    private void InvertSides(int index, SidesState sides, SidesState sidesState)
    {
        sides.edgesInner[index] = !sidesState.edgesInner[index+2];
        sides.edgesOuter[index] = !sidesState.edgesInner[index+2];
        sides.color[index] = sidesState.color[index+2];
    }

    private bool GetRandomBool() => Random.Range(0, 2) == 1;

    private Color GetRandomColor(SidesState sidesState) => sidesState.color[Random.Range(0, 4)];

    private void CheckEdges(SidesState sidesState, int x, int y, int maxX, int maxY)
    {
        if (x == 0)
            EdgeOff(sidesState, 0);
        else if (x == maxX)
            EdgeOff(sidesState, 2);
        if (y == maxY)
            EdgeOff(sidesState, 3);
        else if (y == 0)
            EdgeOff(sidesState, 1);
    }

    private void EdgeOff(SidesState sides, int index)
    {
        sides.edgesInner[index] = true;
        sides.color[index] = Color.white;
        sides.edgesOuter[index] = false;
    }
}
