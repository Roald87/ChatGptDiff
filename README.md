## Getting started

### Developers

1. Add `username/.config/dotnet-tools.json` with the following content

```json
{
  "isRoot": true,
  "tools": {
    "CSharpier": {
      "version": "0.22.1",
      "commands": ["dotnet-csharpier"]
    }
  }
}
```

```
pre-commit install
```
