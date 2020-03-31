lista_1 = [1, 2, 3, 7, 14, 15, 24, 35, 67, 97]
lista_2 = [4, 9, 12, 16, 20, 23, 31, 38, 44, 53]


def funkcja_laczaca_listy(a_list, b_list):
    lista_wyjsciowa = []

    for i in a_list:
        if i % 2 == 0:
            lista_wyjsciowa.append(i)

    for i in b_list:
        if i % 2 != 0:
            lista_wyjsciowa.append(i)

    lista_wyjsciowa.sort()
    return lista_wyjsciowa


print(funkcja_laczaca_listy(lista_1, lista_2))
