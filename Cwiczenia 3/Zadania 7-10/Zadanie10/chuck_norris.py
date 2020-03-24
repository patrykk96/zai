import chucknorris.quips as q


def chuck_norris(name):
    return q.random(name)


inputName = input("Podaj imię: ")
print(chuck_norris(inputName))

