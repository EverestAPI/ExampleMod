module ExampleEffect

using ..Ahorn, Maple

@mapdef Effect "ExampleMod/ExampleEffect" Example(only::String="*", exclude::String="")
#= evaluates to: 
    const Example = Effect{Symbol("YourMod/YourEffect")}
    Example(only::String = "*", exclude::String = "") = 
        Effect{Symbol("YourMod/YourEffect")}("YourMod/YourEffect", only = only, exclude = exclude)
=#

@mapdef Effect "ExampleMod/ExampleEffect2" Example2(only::String="*", exclude::String="", tag::String="", flag::String="", notFlag::String="")

const placements = Example
# `placements` can also be an array to register multiple effects:
#const placements = [Example, Example2]

Ahorn.canFgBg(effect::Example) = true, true

# The order in which attributes will be displayed when editing. 
# Attributes that aren't listed are appended in alphabetical order.
editingOptions(entity::Maple.Effect) = Dict{String, Any}()

# Attributes that should not be displayed when editing.
# `multiple` is true when more than one entity is being edited simultaneously.
editingIgnored(entity::Maple.Effect) = String[]

# Attributes that should not be displayed when editing.
# `multiple` is true when more than one entity is being edited simultaneously.
editingOrder(entity::Maple.Effect) = String[
    "name", "only", "exclude", "tag",
    "flag", "notflag"
]

end