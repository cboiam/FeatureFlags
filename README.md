# Feature flags manager

This application is a demonstration of the techniques proposed by Martin Fowler in this [article](https://www.martinfowler.com/articles/feature-toggles.html), so basically, this application enables a the control of the features of an application.

### How to run

You will need the database serve, so you can run the docker-compose on root folder, or locally.

To run the backend go to `src/FeatureFlag.Api` and then run:

```
dotnet run
```

On `src/FeatureFlag.Web` install node dependencies with:

```
npm install
```

then:

```
ng serve
```
