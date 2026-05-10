using Godot;
using System;
using System.Collections.Generic;

// Author : Roka
public static class MenusManager {

	// Consts

	// Enums
	public const string MENUS = "None,HUD,LevelSelector,Infinite";
	public enum Menus { None, HUD, LevelSelector, Infinite }

	// Variables
	private static Dictionary<Menus, Menu> _DMenus = new();
	private static Menu _currentMenu = null;

	// Functions
	public static void Setup(Menus pName, Menu pMenu) {
		if (!_DMenus.Keys.Contains(pName)) _DMenus.Add(pName, pMenu);
	}

	public static void Switch(Menus pMenu = Menus.None) {
		if (_currentMenu != null) _currentMenu.Visible = false;

		if (!_DMenus.Keys.Contains(pMenu)) pMenu = Menus.None;
		if (pMenu == Menus.None) return;
		
		_currentMenu = _DMenus[pMenu];
		if (_currentMenu != null) _currentMenu.Visible = true;
	}

	// Events
}
