# Introduction



# Build and Test

## Build

1. clone the repo
2. go to src directory
3. build

```
   docker-compose build --build-arg NPMAUTHTOKEN=yourfontawsomeprotoken
```

## Test

1. clone the repo
2. go to src directory
3. build

```
   docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.vs.debug.yml build --build-arg NPMAUTHTOKEN=yourfontawsomeprotoken
```

4. use VSCode Debug to select "Attach to Web(Docker)"

# Contribute


