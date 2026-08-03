filepath = r'c:\Users\USER\Downloads\OCMS\OCMSConsoleApp\Program.cs'
with open(filepath, 'r', encoding='utf-8') as f:
    content = f.read()
content = content.replace('\\"', '"')
with open(filepath, 'w', encoding='utf-8') as f:
    f.write(content)
