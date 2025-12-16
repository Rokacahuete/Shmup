using Godot;

// Author : Roka
public static class MyExtends {
    
    // Consts
    private const string MODULES = "Modules";

    // Variables

    // Functions
    public static T GetModule<T>(this Node tNode) where T : Module {
        Node lModules = tNode.GetNodeOrNull(MODULES);
        T lModuleToReturn = null;

        if (lModules != null)
        foreach (Module lModule in lModules.GetChildren()) {
            if (lModule is not T) continue;

            lModuleToReturn = (T)lModule;
            break;
        }
        return lModuleToReturn;
    }

    public static T GetModule<T>(this Area2D tArea) where T : Module {
        return tArea.GetParent().GetModule<T>();
    }
}