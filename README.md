Adicionei uma alteração nesta primeira linha do ficheiro!

# Aula 4 — Projeto de arranque

Este ficheiro é apenas um guia rápido para abrir o projeto; **não substitui
a ficha de trabalho**, que contém o enunciado, as tarefas e os testes.

## Começar

1. Abrir este projeto no Unity **6000.3.10f1**.
2. Abrir `Assets/Scenes/Aula4_ComunicacaoSistemas.unity` (não `SampleScene`).
3. Executar e validar em **Play Mode**; não é necessária uma build.
4. Consultar a [Ficha de Trabalho — Aula 4](./docs/ficha_aluno.md) para
   os requisitos e a sequência de implementação.
5. Consultar o [guia de entrega por Pull Request](./docs/guia-entrega-pull-request.md)
   para preparar o fork na aula e submeter o trabalho durante a semana.

Cada aluno trabalha num fork próprio. O repositório original é público, mas
apenas o docente tem permissão de escrita. O fork e o Pull Request também
serão públicos.

## Scripts fornecidos

| Script                                           | Estado               | Papel no exercício                                                               |
| ------------------------------------------------ | -------------------- | -------------------------------------------------------------------------------- |
| `IInteractable.cs`                               | Completo             | Contrato de interação.                                                           |
| `PlayerInteraction.cs`                           | Completo             | Deteta triggers e chama o contrato.                                              |
| `Collectible.cs`                                 | Esqueleto compilável | Completar recolha e pedido de pontos.                                            |
| `ScoreSystem.cs`                                 | Esqueleto compilável | Completar pontuação e emissão de `ScoreChanged`.                                 |
| `ScoreDisplay.cs`                                | Esqueleto compilável | Completar subscrição e atualização da UI Toolkit.                                |
| `ScoreDoor.cs`                                   | Esqueleto compilável | Completar abertura ao limiar; a notificação de entrada é o exercício extra.      |
| `PlayerMovement.cs`, `Health.cs`, `UIManager.cs` | Apoio funcional      | Movimento e HUD existente.                                                       |
| `Aula3EnemyMovement.cs`, `DamageZone.cs`         | Reutilizados         | Inimigo da aula anterior, mantido intencionalmente como exemplo de reutilização. |

Os colliders dos collectibles são triggers para que o Player os possa tocar
sem ficar bloqueado. A área de interação também é trigger; evita contabilizar
uma recolha mais do que uma vez.

O inimigo conservou componentes de UI do projeto anterior. São contexto
reutilizado e não fazem parte da tarefa de UI Toolkit da aula 4. Não o
reimplementes; ativa-o em Play Mode apenas quando indicado. Na cena funcional
de referência, o inimigo causa dano quando apanha o Player e a vida a zero
apresenta Game Over.
