
file=open(r'test.txt','w')
file.write('thin\nbis mo \nan')
file.close()
f=open(r'test.txt','r')
print f.read()

f=open(r'test.txt')
lines=f.readlines()
f.close()
str=raw_input("enter your input:")
lines[1]=str+'\n'
f=open(r'test.txt','w')
f.writelines(lines)
f.close()
for item in lines:
    print item
f.close()