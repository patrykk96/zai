lista_1 = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]

lista_2 = list(lista_1[5:])
lista_1 = list(lista_1[:5])

print(lista_1)
print(lista_2)

lista_1.extend(lista_2)
print(lista_1)

lista_1.insert(0, 0)
print(lista_1)

lista_3 = lista_1.copy()
lista_3.sort(reverse=True)
print(lista_3)
