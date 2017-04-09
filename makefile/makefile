object = main.o add.o divide.o minus.o multiply.o
prog = calculate
cc = gcc -g

$(prog) : $(object)
	$(cc) -o calculate $(object)
main.o : main.c math.h
	$(cc) -c main.c
add.o : add.c math.h
	$(cc) -c add.c
divide.o : divide.c math.h
	$(cc) -c divide.c
minus.o : minus.c math.h
	$(cc) -c minus.c
multiply.o : multiply.c math.h
	$(cc) -c multiply.c
clean :
	rm $(prog) $(object)
print: *.c
	lpr -p $?
	touch print
