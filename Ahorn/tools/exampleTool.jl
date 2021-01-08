module ExampleTool

using ..Ahorn, Maple

# !NOT PART OF THE EXAMPLE
begin
    const output = true
    const logMouseMotion = false
    const logSelection = false
    function log(text)
        if !output
            return
        elseif startswith(text, "mouseMotion")
            if logMouseMotion
                println(text)
            end
        elseif startswith(text, "selection")
            if logSelection
                println(text)
            end
        else
            println(text)
        end
    end
end

"""
The name displayed in the tool list
"""
displayName = "Example"

"""
Determines how the tools are grouped in the tool list

Ahorn Groups: Brushes, Placements
"""
group = "Example"

# Ahorn.ListContainer: https://github.com/CelestialCartographers/Ahorn/blob/master/src/helpers/list_view_helper.jl

"""
Called when this tool is selected.

Use this function to populate the `subTools`, `layers`, and `materials` lists, and to set up anything your tool needs while active.
If your tool has a draw function, it should be assigned here to Ahorn.redrawingFuncs["tools"]
"""
toolSelected(subTools::Ahorn.ListContainer, layers::Ahorn.ListContainer, materials::Ahorn.ListContainer) = log("toolSelected: subTools, layers, materials")
toolSelected() = log("toolSelected")

"""
Called when this tool is deselected (when another tool is selected) or when a new room is selected.

Use this function to cleanup any temporary resources your tool uses.
"""
cleanup() = log("cleanup")


# Called when a subtool of this tool is selected
subToolSelected(list::Ahorn.ListContainer, selected::String) = log("subToolSelected: list, selected")

# Called when a layer of this tool is selected
# Appears to be called twice?
layerSelected(list::Ahorn.ListContainer,  materials::Ahorn.ListContainer, selected::String) = log("layerSelected: list, materials, selected=$selected")
layerSelected(list::Ahorn.ListContainer, selected::String) = log("layerSelected: list, selected=$selected")

# Called when a material of this tool is selected
materialSelected(list::Ahorn.ListContainer, selected::String) = log("materialSelected: list, selected=$selected")

# Called when a material of this tool is double clicked
materialDoubleClicked(list::Ahorn.ListContainer,  selected::String) = log("materialDoubleClicked: list, selected=$selected")
materialDoubleClicked(selected::String) = log("materialDoubleClicked: selected=$selected")

# Called when a new room is selected
roomChanged(room::Room) = log("roomChanged: room")
roomChanged(map::Map, room::Room) = log("roomChanged: room, map")

# Called when this tool is selected, or when a new room is selected
# `layers` is the array of layers that can be drawn to in the current room (see Ahorn.DrawableRoom)
# global toolsLayer = Ahorn.getLayerByName(layers, "tools")
layersChanged(layers::Array{Ahorn.Layer, 1}) = log("layersChanged: layers")

# Called when the search bar is used
materialFiltered(list::Ahorn.ListContainer, text::String) = log("toolSelected: list, text=$text")
materialFiltered(list::Ahorn.ListContainer) = log("toolSelected: list")

# Called to determine the materials to be pushed to the top of the materials column
getFavorites() = log("getFavorites")


# Grid Based coordinates
selectionMotion(rect::Ahorn.Rectangle) = log("selectionMotion: rect=$rect")
selectionMotion(x1::Number, y1::Number, x2::Number, y2::Number) = log("selectionMotion: x1=$x1, y1=$y1, x2=$x2, y2=$y2")

# Absolute coordinates
selectionMotionAbs(rect::Ahorn.Rectangle) = log("selectionMotionAbs: rect=$rect")
selectionMotionAbs(x1::Number, y1::Number, x2::Number, y2::Number) = log("selectionMotionAbs: x1=$x1, y1=$y1, x2=$x2, y2=$y2")

# Grid Based coordinates
selectionFinish(rect::Ahorn.Rectangle) = log("selectionFinish: rect=$rect")
selectionFinish(x1::Number, y1::Number, x2::Number, y2::Number) = log("selectionFinishAbs: x1=$x1, y1=$y1, x2=$x2, y2=$y2")

# Absolute coordinates
selectionFinishAbs(rect::Ahorn.Rectangle) = log("selectionFinishAbs: rect=$rect")
selectionFinishAbs(x1::Number, y1::Number, x2::Number, y2::Number) = log("selectionFinishAbs: x1=$x1, y1=$y1, x2=$x2, y2=$y2")


# From Ahorn/event_handler.jl
const mouseHandlers = [
   :leftClick,
   :middleClick,
   :rightClick,
   :backClick,
   :forwardClick
]
# Ahorn.eventMouse = Union{Gtk.GdkEventButton, Gtk.GdkEventMotion, Gtk.GdkEventCrossing}
for handler in mouseHandlers
    @eval $handler() = log($(string(handler)))
    @eval $handler(event::Ahorn.eventMouse, camera::Ahorn.Camera) = log($(string(handler)) * ": event=$(typeof(event)), camera")
    # Grid Based coordinates
    @eval $handler(x::Number, y::Number) = log($(string(handler)) * ": x=$x, y=$y")
    # Absolute coordinates
    @eval $(Symbol(string(handler) * "Abs"))(x::Number, y::Number) = log($(string(handler) * "Abs") * ": x=$x, y=$y")
end


mouseMotion(event::Ahorn.eventMouse, camera::Ahorn.Camera) = log("mouseMotion: event=$(typeof(event)), camera")
mouseMotion(x::Number, y::Number) = log("mouseMotion: x=$x, y=$y")
mouseMotionAbs(event::Ahorn.eventMouse, camera::Ahorn.Camera) = log("mouseMotionAbs: event=$(typeof(event)), camera")
mouseMotionAbs(x::Number, y::Number) = log("mouseMotionAbs: x=$x, y=$y")


# Ahorn.eventKey = Gtk.GdkEventKey
keyboard(event::Ahorn.eventKey) = log("keyboard: event=$(event.hardware_keycode)")

end