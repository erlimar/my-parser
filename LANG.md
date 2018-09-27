Com esse parser você poderia construir uma linguagem louca tipo:


```lang
nome = algo

"nome" recebe "algo"

nome = 89

nome = program|tipoA=>nome, tipoB=>sobrenome| (
	nome = nome
	sobre = sobrenome
)

nome = program|tipoA, tipoB| (
	nome = _0
	sobre = _1
	
	=> exec concat(nome, " ", sobre)
)

revert = program|tipo| ( ... )

exec nome(12, "valor")
	=> revert
	=> sys_print

	
a + b => c
# translated to: exec sys_op_plus(arg0, arg1) => c

c++ => d
# translated to: exec sys_op_plusplus(arg0) => d

my_type = def type (
    valueA = "outro"
)

my_type = def type | str=>valor | (
    valueA = valor
)

my_op_plus = def between operator "+" |my_type, my_type| (
	=> exec concat(_0.valueA, _1.valueA)
	=> my_type
	# translated to:
	| exec concat(_0.valueA, _1.valueA) => tmp
	| => exec my_type(tmp)
)

myA = exec my_type
myB = exec my_type

myA + myB => myC
```
