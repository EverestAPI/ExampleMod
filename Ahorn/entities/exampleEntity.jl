module ExampleEntity

using ..Ahorn, Maple

#=
Minimum valid declaration.

This defines a type `Example` with a constructor that takes in `x` and `y`,
which then creates a unique `Entity` with the name `"ExampleMod/ExampleEntity"` 
and a Dict containing the keys x and y, set to the values passed to the `Example` constructor.
=#
@mapdef Entity "ExampleMod/ExampleEntity" Example(x::Number, y::Number)
#= equivalent to: 
    @pardef Example(x::Number, y::Number) = Entity("ExampleMod/ExampleEntity", x=x, y=y)
=#
#= evaluates to:
    const Example = Entity{Symbol("ExampleMod/ExampleEntity")}
    Example(x::Number, y::Number) = Entity{Symbol("ExampleMod/ExampleEntity")}("ExampleMod/ExampleEntity", x = x, y = y)
=# 

#=
Entity data has the following reserved attributes: x, y, width, height, nodes.
All attributes other than x and y must be given a default value.
Allowed attribute types (ones that won't break the map parser) include:
    String, Bool, Char, Int, Float
=#
@mapdef Entity "ExampleMod/ExampleEntity2" Example2(x::Number, y::Number, width::Number=16, height::Number=16, nodes::Tuple{Int, Int}[]=[(0,0)], attrib::Bool=false)
#= equivalent to: 
    @pardef Example2(x::Number, y::Number, width::Number=16, height::Number=16, nodes::Tuple{Int, Int}[]=[(0,0)], attrib::Bool=false) = 
        Entity("ExampleMod/ExampleEntity", x=x, y=y, width=width, height=height, nodes=nodes, attrib=attrib)
=#
#= evaluates to:
    const Example2 = Entity{Symbol("ExampleMod/ExampleEntity2")}
    Example2(x::Number, y::Number, width::Number = 16, height::Number = 16, nodes::Tuple{Int, Int}[] = [(0, 0)], attrib::Bool = false) = 
        Entity{Symbol("ExampleMod/ExampleEntity2")}("ExampleMod/ExampleEntity2", x = x, y = y, width = width, height = height, nodes = nodes, attrib = attrib)
=#

# On load, EntityPlacements are retrieved from the placements field of each module and combined into a list.
const placements = Ahorn.PlacementDict(
    "Example Entity" => Ahorn.EntityPlacement(
        Example,                    # Your Entity type
        "point",                    # One of: "point", "rectangle", "line"
        Dict{String, Any}(),        # Data specific to this placement
        function finalizer(args...) # Called when the entity is created
            # arguments can be any of:
            #    (target, map, room)
            #    (target, room)
            #    (target)
        end
    )
)

#= Render the entity in Ahorn.
render is relative to `Ahorn.position(entity)`, renderAbs is relative to 0,0

Drawing helper methods provided by Ahorn can be found here:
https://github.com/CelestialCartographers/Ahorn/blob/12f6ac29677dc1f3c05b468bd205b757cbd4ec1e/src/drawing.jl#L223
=#
Ahorn.render(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.render(ctx::Cairo.CairoContext, entity::Example, room::Room) = false
Ahorn.renderAbs(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.renderAbs(ctx::Cairo.CairoContext, entity::Example, room::Room) = false

# Render the entity while it is selected.
Ahorn.renderSelected(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.renderSelected(ctx::Cairo.CairoContext, entity::Example, room::Maple.Room) = false
Ahorn.renderSelectedAbs(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.renderSelectedAbs(ctx::Cairo.CairoContext, entity::Example, room::Maple.Room) = false

#=
Describe the selection box for your entity.
Supported return types:
    Ahorn.Rectangle, Ahorn.Rectangle[], Bool (does nothing)
=#
Ahorn.selection(entity::Example) = nothing
Ahorn.selection(entity::Example, room::Maple.Room) = nothing

# The minimum width and height allowed for the entity. Can be overridden with debug settings.
Ahorn.minimumSize(entity::Example) = 8, 8
# Whether to allow resizing the x and y axis. Can be overridden with debug settings.
Ahorn.resizable(entity::Example) = false, false

# The minimum and maximum number of allowed nodes. `-1` for unbounded.
Ahorn.nodeLimits(entity::Example) = 0, 0

#=
Add specific editing options for attributes.
Supported options include:
    Array, Dict
=#
Ahorn.editingOptions(entity::Example) = Dict{String, Any}(
    "attributeName" => ["opt1", "opt2", "opt3"]
)

# The order in which attributes will be displayed when editing. 
# Attributes that aren't listed are appended in alphabetical order.
Ahorn.editingOrder(entity::Example) = String["x", "y", "width", "height"]

# Attributes that should not be displayed when editing.
# `multiple` is true when more than one entity is being edited simultaneously.
Ahorn.editingIgnored(entity::Example, multiple::Bool=false) = multiple ? String["x", "y", "width", "height", "nodes"] : String[]

# `deleted`, `moved`, and `resized` are callbacks provided by Ahorn for the corresponding actions.
Ahorn.deleted(entity::Example, node::Int) = nothing
Ahorn.moved(entity::Example) = nothing
Ahorn.moved(entity::Example, x::Int, y::Int) = nothing
Ahorn.resized(entity::Example) = nothing
Ahorn.resized(entity::Example, width::Int, height::Int) = nothing

# If an entity is returned, it replaces the current one
Ahorn.flipped(entity::Example, horizontal::Bool) = nothing

# If an entity is returned, it replaces the current one
Ahorn.rotated(entity::Example, steps::Int) = nothing

end