def reverse(input):
    strLen = len(input)
    reversedString = ""
    while strLen > 0:
        reversedString += input[strLen - 1]
        strLen -= 1
    return reversedString


data = input("Podaj słowo do odwrócenia:")
reversed = reverse(data)
print(reversed)


