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
=#
@mapdef Entity "ExampleMod/ExampleEntity2" Example2(x::Number, y::Number, width::Number=16, height::Number=16, nodes::Tuple{Int, Int}=(0,0), attrib::Bool=false)
#= equivalent to: 
    @pardef Example2(x::Number, y::Number, width::Number=16, height::Number=16, nodes::Tuple{Int, Int}=(0,0), attrib::Bool=false) = 
        Entity("ExampleMod/ExampleEntity", x=x, y=y, width=width, height=height, nodes=nodes, attrib=attrib)
=#
#= evaluates to:
    const Example2 = Entity{Symbol("ExampleMod/ExampleEntity2")}
    Example2(x::Number, y::Number, width::Number = 16, height::Number = 16, nodes::Tuple{Int, Int} = (0, 0), attrib::Bool = false) = 
        Entity{Symbol("ExampleMod/ExampleEntity2")}("ExampleMod/ExampleEntity2", x = x, y = y, width = width, height = height, nodes = nodes, attrib = attrib)
=#

"""
On load, EntityPlacements are retrieved from the placements field of each module and combined into a list.
"""
const placements = Ahorn.PlacementDict(
    "Example Entity" => Ahorn.EntityPlacement(
        Example,                    # Your Entity type
        "point",                    # One of: "point", "rectangle", "line"
        Dict{String, Any}(),        # Data specific to this placement
        function finalizer(args...) # Called when the entity is created
            # arguments can be any of:
            #    (target, map, room),
            #    (target, room),
            #    (target,)
        end
    )
)

Ahorn.renderAbs(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.renderAbs(ctx::Cairo.CairoContext, entity::Example, room::Room) = false
Ahorn.render(ctx::Cairo.CairoContext, entity::Example) = false
Ahorn.render(ctx::Cairo.CairoContext, entity::Example, room::Room) = false

Ahorn.renderSelected(ctx::Cairo.CairoContext, entity::Maple.Entity) = false
Ahorn.renderSelected(ctx::Cairo.CairoContext, entity::Maple.Entity, room::Maple.Room) = false
Ahorn.renderSelectedAbs(ctx::Cairo.CairoContext, entity::Maple.Entity) = false
Ahorn.renderSelectedAbs(ctx::Cairo.CairoContext, entity::Maple.Entity, room::Maple.Room) = false

end