# mdp-value-iteration
Basic implementation of Markov Decision Processes using Values Iteration algorithm with graphic representation in Unity

# Basic usage

Open Unity project and find **MPD** gameObject in the hierarchy. There is **MDP** script attached to this gameObject. There are some values to change:

![MDP1](/img/1.png)

- **Penatly** - reward/penalty for each step
- **Probability** - probability of choosing best action 
- **Discount factor** - gamma parameter in Bellman's quation
- **Rows/Columns** - size of enviroment table
- **Good/Bad Rewards** - value of preferable/unpreferable state
- **Epsilon** - stops the algorithm when absolute value of changes is less than that value

Use **left mouse butto**n to create preferable/unpreferable states by clicking on cells. Use **right mouse button** to create walls. Use **spacebar** to start the algorithm. Morover after pressing **mouse wheel** arows with directions (policy) will be shown.

<img src="/img/2.png" width="600"/> 
<img src="/img/3.png" width="600"/>
